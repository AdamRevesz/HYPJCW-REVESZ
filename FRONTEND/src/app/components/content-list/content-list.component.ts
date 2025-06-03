import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ApiService } from '../../services/api.service';
import { ContentShortViewDto } from '../../models/content.model'; 
import { RouterLink } from '@angular/router';
import { Subscription } from 'rxjs';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-content-list',
  standalone: true,
  imports: [CommonModule, RouterLink,
    FormsModule
  ],
  templateUrl: './content-list.component.html',
  styleUrls: ['./content-list.component.scss']
})
export class ContentListComponent implements OnInit {
  private apiService = inject(ApiService);
  private contentAddedSubscription!: Subscription;

  contents: ContentShortViewDto[] = [];
  content: ContentShortViewDto | null = null;
  isLoading: boolean = false;
  error: string | null = null;
  deleteError: string | null = null;
  deletingItemId: string | null = null;
  creatorDict: {[key: string]: ContentShortViewDto } = {};
  username: string = '';
  category: string = '';

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
    this.creatorDict = {};

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

  filterByCategory(tag: string){
    this.contents = [];

    this.apiService.getContentByCategory(tag).subscribe({
      next: (data)=>{
        this.contents = data;
        this.isLoading = false;
        console.log('Data loaded successfully');
      },
      error: (err)=>{
        console.error('Error fetching data from filter', err);
        this.error = ('Error while fetching data');
        this.isLoading = false;
      },
      complete: ()=>{
        console.log('data fetched usccessfully');
      }
    });
  }

  getHighestApprovedContents(){
    this.contents = [];
    this.apiService.getHighestApprovedContents().subscribe({
      next: (data) => {
        this.contents = data;
        this.isLoading = false;
        console.log('Data fetched successfully');
      },
      error: (err)=>{
        console.error('Error while fetching data', err);
        this.error = ('Error while fetching data');
        this.isLoading = false;
      },
      complete: () =>{
        console.log('Data fetched successfully');
      }
    });
  }

  getHighestApprovedContentByUser(username: string){
    this.contents = [];
    this.content = null;
    this.creatorDict = {};
    this.apiService.getContentHighestRatedContentOfUser(username).subscribe({
      next: (data) =>{
        this.content = data;
        this.isLoading = false;
        console.log('Data fetched successfully');
        this.contents.push(this.content)
      },
      error: (err) =>{
        console.error('Failed to fetch data', err);
        this.error = ('Error while loading data');
        this.isLoading = false;
      },
      complete:()=>{
        console.log('Data fetched usccessfully');
      }
    });
  }

  getHighestApprovedCreator(): void {
    this.isLoading = true;
    this.error = null;
    this.creatorDict = {};
    this.contents = [];

    this.apiService.getHighestApprovedCreator().subscribe({
      next: (data) => {
        this.creatorDict = data;
        this.isLoading = false;
        console.log('Creator content dictionary fetched successfully:', data);
        const creatorName = Object.keys(data)[0];
        const creatorContent = Object.values(data)[0];
        this.contents.push(creatorContent);
      },
      error: (err) => {
        console.error('Error fetching creator content dictionary:', err);
        this.error = 'Failed to load creator content data';
        this.isLoading = false;
      },
      complete: () => {
        console.log('Creator content dictionary fetching completed');
      }
    });
  }

  getTopCreatorName(): string | null {
  const keys = Object.keys(this.creatorDict);
  return keys.length > 0 ? keys[0] : null;
}

getTopCreatorContent(): ContentShortViewDto | null {
  const values = Object.values(this.creatorDict);
  return values.length > 0 ? values[0] : null;
}

searchUserContent(): void{
  if(!this.username?.trim()){
    return;
  }
  this.getHighestApprovedContentByUser(this.username.trim());
}

searchContentCategory():void{
  if(!this.category?.trim()){
    return;
  }
  this.filterByCategory(this.category.trim());
}

}