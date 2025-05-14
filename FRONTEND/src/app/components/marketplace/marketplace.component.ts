import { Component, OnInit, OnDestroy, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ApiService } from '../../services/api.service';
import { SalesItemShortViewDto } from '../../models/content.model'; 
import { RouterLink } from '@angular/router';
import { Subscription } from 'rxjs';


@Component({
  selector: 'app-marketplace',
  standalone: true,
  imports: [
    CommonModule,
    RouterLink
  ],
  templateUrl: './marketplace.component.html',
  styleUrls: ['./marketplace.component.scss']
})
export class MarketplaceComponent implements OnInit, OnDestroy {
  private apiService = inject(ApiService);
  private contentAddedSubscription!: Subscription;

  contents: SalesItemShortViewDto[] = [];
  isLoading: boolean = false;
  error: string | null = null;
  deleteError: string | null = null;
  deletingItemId: string | null = null;

  ngOnInit(): void {
    this.loadAllContent();
    this.contentAddedSubscription = this.apiService.contentAdded$.subscribe(() => {
      console.log('Refreshing marketplace list due to new content');
      this.loadAllContent();
    });
  }

  ngOnDestroy(): void {
    if (this.contentAddedSubscription) {
      this.contentAddedSubscription.unsubscribe();
    }
  }

  loadAllContent(): void {
    this.isLoading = true;
    this.error = null;
    this.contents = []; 

    this.apiService.getSalesItems().subscribe({
      next: (data: SalesItemShortViewDto[]) => { 
        console.log('Data received from getSalesItems:', data); 
        if (data && Array.isArray(data)) {
          this.contents = data;
        } else {
          console.warn('Received non-array data or null/undefined from getSalesItems. Setting contents to empty array.');
          this.contents = [];
        }
        this.isLoading = false;
      },
      error: (err) => {
        console.error('Error fetching sales items:', err);
        this.error = 'Failed to load sales items. Please try again later.';
        this.isLoading = false;
      },
      complete: () => {
        if (this.isLoading) { 
            this.isLoading = false;
        }
        console.log('getSalesItems subscription completed.');
      }
    });
  }

  deleteContentItem(id: string): void {
    this.deletingItemId = id;
    this.deleteError = null;

    this.apiService.deleteContent(id).subscribe({
      next: () => {
        console.log(`Sales item with ID: ${id} deleted successfully.`);
        this.loadAllContent();
        this.deletingItemId = null;
      },
      error: (err) => {
        console.error(`Error deleting sales item with ID: ${id}`, err);
        this.deleteError = `Failed to delete item. ${err.message || 'Please try again.'}`;
        this.deletingItemId = null;
      }
    });
  }

  confirmDelete(id: string, title?: string): void {
    const itemTitle = title || 'this item';
    if (confirm(`Are you sure you want to delete "${itemTitle}"? This action cannot be undone.`)) {
      this.deleteContentItem(id);
    }
  }
}