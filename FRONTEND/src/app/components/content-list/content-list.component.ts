import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ApiService } from '../../services/api.service';
import { ContentShortViewDto } from '../../models/content.model'; 
import { RouterLink } from '@angular/router';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-content-list',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './content-list.component.html',
  styleUrls: ['./content-list.component.scss']
})
export class ContentListComponent implements OnInit {
  private apiService = inject(ApiService);
  private contentAddedSubscription!: Subscription;

  contents: ContentShortViewDto[] = [];
  isLoading: boolean = false;
  error: string | null = null;
  deleteError: string | null = null;
  deletingItemId: string | null = null;

  ngOnInit(): void {
    this.loadAllContent();
    this.contentAddedSubscription = this.apiService.contentAdded$.subscribe(()=>
    {
      console.log('Refreshing list');
      this.loadAllContent();
    })
  }

  ngOnDestroy():void {
    if(this.contentAddedSubscription){
      this.contentAddedSubscription.unsubscribe();
    }
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

 deleteContentItem(id: string): void {
    this.deletingItemId = id;
    this.deleteError = null;

    this.apiService.deleteContent(id).subscribe({
      next: () => {
        console.log(`Content item with ID: ${id} deleted successfully.`);
       
        this.deletingItemId = null;
      },
      error: (err) => {
        console.error(`Error deleting content item with ID: ${id}`, err);
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