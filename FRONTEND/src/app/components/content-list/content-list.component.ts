import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ApiService } from '../../services/api.service';
import { ContentShortViewDto } from '../../models/content.model'; 
import { RouterLink } from '@angular/router';
import { Router } from 'express';

@Component({
  selector: 'app-content-list',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './content-list.component.html',
  styleUrls: ['./content-list.component.scss']
})
export class ContentListComponent implements OnInit {
  private apiService = inject(ApiService);

  contents: ContentShortViewDto[] = [];
  isLoading: boolean = false;
  error: string | null = null;

  ngOnInit(): void {
    this.loadAllContent();
  }

  loadAllContent(): void {
    this.isLoading = true;
    this.error = null;
    this.contents = [];

    this.apiService.getContent().subscribe({
      next: (data) => {
        this.contents = data;
        this.isLoading = false;
        console.log('Content fetched successfully:', data);
      },
      error: (err) => {
        console.error('Error fetching content:', err);
        this.error = 'Failed to load content. Please try again later.';
        this.isLoading = false;
      },
      complete: () => {
        console.log('Content fetching completed.');
      }
    });
  }
}