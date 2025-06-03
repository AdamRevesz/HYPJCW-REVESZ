import { Component, OnInit, OnDestroy, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ApiService } from '../../services/api.service';
import { ContentCategoryCountDto, UserContentCountDto } from '../../models/content.model';
import { NgApexchartsModule } from 'ng-apexcharts';

@Component({
  selector: 'app-graph',
  imports: [
    CommonModule,
    NgApexchartsModule
  ],
  templateUrl: './graph.component.html',
  styleUrl: './graph.component.scss'
})
export class GraphComponent implements OnInit, OnDestroy {
  private apiService = inject(ApiService);

  isLoading: boolean = false;
  error: string | null = null;
  public chartOptions: any = null;
  contentCategory: ContentCategoryCountDto[] = [];
  userContentCount: UserContentCountDto[] = [];

  ngOnInit(): void {

  }

  ngOnDestroy(): void {
  }

  // Generic chart builder method
  buildChart(
    data: any[],
    labelProperty: string,
    valueProperty: string,
    chartTitle: string = 'Chart',
    chartType: 'bar' | 'line' | 'area' | 'pie' = 'bar',
    yAxisTitle: string = 'Values'
  ): void {

    const labels = data.map(item => String(item[labelProperty]));
    const values = data.map(item => Number(item[valueProperty]));

   this.chartOptions = {
      series: chartType === 'pie' ? values : [{
        name: yAxisTitle,
        data: values
      }],
      chart: {
        height: 350,
        type: chartType,
      },
      colors: ['#5c2ca8', '#4a238a', '#6b46c1', '#7c3aed', '#8b5cf6', '#a78bfa'],
      labels: chartType === 'pie' ? labels : undefined,
      xaxis: chartType !== 'pie' ? {
        categories: labels,
        axisBorder: {
          show: true,
          color: '#5c2ca8'
        },
        axisTicks: {
          show: true,
          color: '#5c2ca8'
        }
      } : undefined,
      yaxis: chartType !== 'pie' ? {
        title: {
          text: yAxisTitle
        },
        axisBorder: {
          show: true,
          color: '#5c2ca8'
        },
        axisTicks: {
          show: true,
          color: '#5c2ca8'
        }
      } : undefined,
      grid: chartType !== 'pie' ? {
        show: true,
        borderColor: '#e0e0e0',
        xaxis: { lines: { show: true } },
        yaxis: { lines: { show: true } }
      } : undefined,
      title: {
        text: chartTitle,
        align: 'center'
      },
      dataLabels: {
        enabled: chartType === 'pie'
      },
      legend: {
        show: chartType === 'pie'
      }
    };
  }

  // Load user content data
  loadUserContentData(): void {
    this.isLoading = true;
    this.error = null;
    this.chartOptions = null;

    this.apiService.getUserContentCount().subscribe({
      next: (data) => {
        this.buildChart(
          data,
          'username',
          'contentCount',
          'User Content Statistics',
          'bar',
          'Content Count'
        );
        this.isLoading = false;
      },
      error: (err) => {
        console.error('Error loading user content data:', err);
        this.error = 'Error loading user content data';
        this.isLoading = false;
        this.chartOptions = null;
      }
    });
  }


  // Load category data
  loadCategoryData(): void {
    this.isLoading = true;
    this.error = null;
    this.chartOptions = null;

    this.apiService.getContentCategoryCount().subscribe({
      next: (data) => {
        console.log('Data fetched')
        this.contentCategory = data;
        this.isLoading = false;
        this.buildChart(
          data,
          'category',
          'count',
          'Categories',
          'bar',
          'Content Category Count',
        );
      },
      error: (err) => {
        console.error('Data fetch error', err);
        this.error = 'Error loading data';
        this.isLoading = false;
      },
    })
  }
}