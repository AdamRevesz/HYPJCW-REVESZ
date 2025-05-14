import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common'; 
import { ReactiveFormsModule } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';
import { ApiService } from '../../services/api.service';
import {Router} from '@angular/router';

@Component({
  selector: 'app-sales-upload',
  imports: [
    CommonModule,
    ReactiveFormsModule
  ],
  templateUrl: './sales-upload.component.html',
  styleUrl: './sales-upload.component.scss'
})
export class SalesItemCreateDto implements OnInit {
  pictureForm!: FormGroup;
  imagePreview: string | null = null;
  uploadError: string | null = null;
  selectedFile: File | null = null;
  isSubmitting = false;
  
  constructor(
    private fb: FormBuilder,
    private apiService: ApiService,
    private router: Router,
  ) {}
  
  ngOnInit(): void {
    this.pictureForm = this.fb.group({
      title: ['', Validators.required],
      body: ['', Validators.required],
      category: ['', Validators.required],
      tags: [''],
      ownerId: ['user123'] 
    });
  }
  
  onFileSelected(event: any): void {
    const file = event.target.files[0];
    if (file) {
      if (!file.type.match(/image\/(jpeg|jpg|png|gif|webp)/)) {
        this.uploadError = 'Please select a valid image file (JPEG, PNG, GIF, or WebP).';
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
    if (this.pictureForm.invalid || !this.selectedFile) {
      Object.keys(this.pictureForm.controls).forEach(key => {
        this.pictureForm.get(key)?.markAsTouched();
      });
      
      if (!this.selectedFile) {
        this.uploadError = 'Please select an image file.';
      }
      return;
    }
    
    this.isSubmitting = true;
    this.uploadError = null;
    
    const formData = new FormData();
    formData.append('uploadedFile', this.selectedFile);
    
    Object.keys(this.pictureForm.value).forEach(key => {
      formData.append(key, this.pictureForm.get(key)?.value);
    });
    
    this.apiService.addSalesItem(formData).subscribe({
      next: () => {
        console.log('Sales Item added succesfully');
        this.resetForm();
        this.isSubmitting = false;
        this.router.navigate(['/list']);
      },
      error: (error: HttpErrorResponse) => {
        this.uploadError = `Failed to upload Item: ${error.message || 'Unknown error'}`;
        this.isSubmitting = false;
      }
    });
  }
  
  resetForm(): void {
    this.pictureForm.reset({
      ownerId: 'user123'
    });
    this.imagePreview = null;
    this.selectedFile = null;
  }
}
