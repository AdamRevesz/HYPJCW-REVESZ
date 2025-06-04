import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { ApiService } from '../../services/api.service';
import { HttpErrorResponse } from '@angular/common/http';
import { UserViewDto } from '../../models/content.model';

@Component({
  selector: 'app-user-profile',
  imports: [
    CommonModule,
    ReactiveFormsModule
  ],
  templateUrl: './user-profile.component.html',
  styleUrl: './user-profile.component.scss'
})

export class UserProfileComponent implements OnInit {
  userForm!: FormGroup;
  uploadError: string | null = null;
  selectedFile: File | null = null;
  imagePreview: string | null = null;
  isSubmiting = false;
  userId = 'adda4670-e23d-43b0-a6f5-de4ff095fd53';
  user: UserViewDto | null = null;
  userLoadError: string | null = null;

  constructor(
    private fb: FormBuilder,
    private apiService: ApiService
  ) { }

  ngOnInit(): void {
    this.userForm = this.fb.group({
      username: [''],
      email: ['', Validators.email],
      password: [''],
      isProfessional: [false]
    });

    this.loadUser();
  }

  loadUser(): void {
    this.apiService.getUser(this.userId).subscribe({
      next: (data) => {
        this.user = data;
        console.log('User is loaded', this.user);
        this.userForm.patchValue({
          username: this.user.username || '',
          emailAddress: '',
          password: '',
          isProfessional: this.user.isProfessional || false
        });

        this.userLoadError = null;
      },
      error: (err) => {
        console.log(`Error loading in user ID: ${this.userId}`, err);
        this.userLoadError = 'Failed to load user information';
      }
    })
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
  }  onSubmit(): void {
    this.isSubmiting = true;
    this.uploadError = null;

    const userForm = new FormData();
    
    const formData = this.userForm.value;
    if (formData.username && formData.username.trim()) {
      userForm.append('username', formData.username.trim());
    }
    if (formData.emailAddress && formData.emailAddress.trim()) {
      userForm.append('email', formData.emailAddress.trim());
    }
    if (formData.password && formData.password.trim()) {
      userForm.append('password', formData.password.trim());
    }
    userForm.append('isProfessional', formData.isProfessional.toString());

    this.apiService.updateUser(this.userId, userForm).subscribe({
      next: () => {
        console.log('User profile updated');
        this.isSubmiting = false;
        this.loadUser();
        this.uploadError = 'Profile updated successfully!';
        setTimeout(() => this.uploadError = null, 3000);
      },
      error: (error: HttpErrorResponse) => {
        this.uploadError = `Failed to update profile: ${error.message}`;
        this.isSubmiting = false;
      }
    });
  }

  updateProfilePicture(): void {
    if (!this.selectedFile) {
      this.uploadError = 'Please select an image file first.';
      return;
    }

    this.isSubmiting = true;
    this.uploadError = null;

    const pictureFormData = new FormData();
    pictureFormData.append('uploadedFile', this.selectedFile);

    this.apiService.updateUserPicture(this.userId, pictureFormData).subscribe({
      next: () => {
        console.log('Profile picture updated successfully');
        this.resetForm();
        this.isSubmiting = false;
      },
      error: (err: HttpErrorResponse) => {
        this.uploadError = `Failed to update profile picture: ${err.message}`;
        this.isSubmiting = false;
      }
    });
  }

  resetForm(): void {
    this.userForm.reset();
    this.selectedFile = null;
    this.imagePreview = null;
    this.uploadError = null;
  }
}
