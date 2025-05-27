import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';

import { ApiService } from '../../services/api.service';
import { CommentViewDto, ContentViewDto, CommentCreateDto } from '../../models/content.model';

@Component({
  selector: 'app-inspect-item',
  standalone: true,
  imports: [
    CommonModule,
    RouterLink,
    ReactiveFormsModule
  ],
  templateUrl: './inspect-item.component.html',
  styleUrl: './inspect-item.component.scss'
})
export class InspectItemComponent implements OnInit {
  private apiService = inject(ApiService);
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private fb = inject(FormBuilder);

  content: ContentViewDto | null = null;
  comments: CommentViewDto[] = [];
  isLoading: boolean = false;
  isSubmittingComment: boolean = false;
  isLoadingComments: boolean = false;
  error: string | null = null;
  deleteCommentError: string | null = null;
  deletingCommentId: string | null = null;

  commentForm!: FormGroup;

  ngOnInit(): void {
    this.commentForm = this.fb.group({
      commentText: ['', Validators.required]
    });

    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.loadContentAndInitialComments(id);
    } else {
      this.error = 'Error: No content ID found in URL.';
      console.error('No ID found in route parameters');
    }
  }

  loadContentAndInitialComments(id: string): void {
    this.isLoading = true;
    this.error = null;

    this.apiService.getContentById(id).subscribe({
      next: (content: ContentViewDto) => {
        this.content = content;
        console.log('Content loaded successfully:', content);
        this.loadComments(id);
      },
      error: (err: HttpErrorResponse) => {
        console.error('Error loading content:', err);
        this.error = `Failed to load content. ${err.error?.message || err.message || 'Please try again'}`;
        this.isLoading = false;
      }
    });
  }

  loadComments(id: string): void {
    this.isLoadingComments = true;
    
    this.apiService.getComment(id).subscribe({
      next: (comments: CommentViewDto[]) => {
      this.comments = comments.sort((a, b) => new Date(b.createdAt).getTime() - new Date(a.createdAt).getTime()); //!!Don't forget to add sorting option based on likes too!!
      console.log('Comments loaded and sorted successfully:', this.comments);
      this.isLoadingComments = false;
      this.isLoading = false;
      },
      error: (err: HttpErrorResponse) => {
      console.error('Error loading comments:', err);
      this.error = `Failed to load comments. ${err.error?.message || err.message || 'Please try again'}`;
      this.isLoadingComments = false;
      this.isLoading = false;
      }
    });
  }

  submitComment(): void {
    if(!this.content || !this.content.id) {
      this.error = 'Cannot submit comment'
      return;
    }

    if (this.commentForm.invalid) {
      this.error = 'Please enter a comment before submitting.';
      return;
    }

    this.isSubmittingComment = true;
    this.error = null;
    
    const commentData: CommentCreateDto = {
      body: this.commentForm.get('commentText')?.value
    };

    const currentContentId = this.content.id
    this.apiService.addCommentToContent(currentContentId, commentData).subscribe({
      next: () => {
        console.log('Comment submitted successfully');
        this.commentForm.reset();
        this.isSubmittingComment = false;
        this.loadComments(currentContentId);
      },
      error: (err: HttpErrorResponse) =>{
        console.error('Error submitting comment:', err);
        this.error = `Failed to submit comment. ${err.error?.message || err.message || 'Please try again'}`;
        this.isSubmittingComment = false;
      }
    });
  }

  deleteComment(commentId: string): void {
    if (!this.content || !this.content.id) {
      this.deleteCommentError = 'Cannot delete comment';
      return;
    }
    
    this.deletingCommentId = commentId;
    this.deleteCommentError = null;

    this.apiService.deleteComment(commentId).subscribe({
      next: () => {
        console.log(`Comment deleted successfully`);
        this.deletingCommentId = null;
        this.loadComments(this.content!.id);
      },
      error: (err: HttpErrorResponse) => {
        console.error(`Error deleting comment:`, err);
        this.deleteCommentError = `Failed to delete comment. ${err.error?.message || err.message || 'Please try again'}`;
        this.deletingCommentId = null;
      }
    });
  }

  addToBasket(): void {

  }

  likeContent(): void {

  }

  dislikeContent(): void {

  }
}