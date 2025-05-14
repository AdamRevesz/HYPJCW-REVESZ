import { Component, OnInit, inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { HttpErrorResponse } from '@angular/common/http';
import { ApiService } from '../../services/api.service';
import { ActivatedRoute, Router } from '@angular/router';
import { SalesItemShortViewDto } from '../../models/content.model';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-marketplace-edit',
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RouterLink
  ],
  templateUrl: './marketplace-edit.component.html',
  styleUrl: './marketplace-edit.component.scss'
})
export class MarketplaceEditComponent implements OnInit {
  itemForm!: FormGroup;
  imagePreview: string | null = null;
  uploadError: string | null = null;
  selectedFile: File | null = null;
  isSubmitting = false;

  salesItem: SalesItemShortViewDto | null = null;
  private salesItemId: string | null = null;

  private fb = inject(FormBuilder);
  private apiService = inject(ApiService);
  private router = inject(Router);
  private route = inject(ActivatedRoute);

  ngOnInit(): void {
    this.itemForm = this.fb.group({
      title: ['', Validators.required],
      body: ['', Validators.required],
      type: ['', Validators.required],
      price: ['', Validators.required]
    });

    this.salesItemId = this.route.snapshot.paramMap.get('id');

    if (this.salesItemId) {
      this.loadsalesItem(this.salesItemId);
    } else {
      this.uploadError = 'No item ID found to edit.';

    }
  }

  loadsalesItem(id: string): void {
    this.isSubmitting = true;
    this.apiService.getSalesItemById(id).subscribe({
      next: (data) => {
        this.salesItem = data;
        this.imagePreview = data.filePath;
        this.itemForm.patchValue({
          title: data.title,
          body: data.body,
          price: data.price,
          type: data.type
        });
        if (data.filePath) {
          this.imagePreview = data.filePath;
        }
        this.isSubmitting = false;
      },
      error: (err) => {
        this.uploadError = `Failed to load item details: ${err.message}`;
        this.isSubmitting = false;
      }
    });
  }

  onFileSelected(event: any): void {
    const file = event.target.files[0];
    if (file) {
      if (!file.type.match(/image\/(jpeg|jpg|png|gif|webp)/)) {
        this.uploadError = 'Please select a valid image file (JPEG, PNG, GIF, or WebP).';
        this.selectedFile = null;
        this.imagePreview = this.salesItem?.filePath || null; 
        return;
      }
      
      this.selectedFile = file;
      this.uploadError = null;
      
      const reader = new FileReader();
      reader.onload = () => {
        this.imagePreview = reader.result as string;
      };
      reader.readAsDataURL(file);
    }
  }
  
  onSubmit(): void {
    if (!this.salesItemId) {
      this.uploadError = "Cannot update item: ID is missing.";
      return;
    }

    if (this.itemForm.invalid) {
      Object.keys(this.itemForm.controls).forEach(key => {
        this.itemForm.get(key)?.markAsTouched();
      });
      this.uploadError = "Please correct the errors in the form.";
      return;
    }
    
    this.isSubmitting = true;
    this.uploadError = null;
    
    const formData = new FormData();

    if (this.selectedFile) {
      formData.append('uploadedFile', this.selectedFile, this.selectedFile.name);
    }
    
    Object.keys(this.itemForm.value).forEach(key => {
      let value = this.itemForm.get(key)?.value;
      formData.append(key, value);
    });
    
    this.apiService.editSalesItem(this.salesItemId, formData).subscribe({
      next: () => {
        console.log('Item updated successfully');
        this.isSubmitting = false;
        this.router.navigate(['/']);
      },
      error: (error: HttpErrorResponse) => {
        this.uploadError = `Failed to update item: ${error.message || 'Unknown error'}`;
        this.isSubmitting = false;
      }
    });
  }
}
