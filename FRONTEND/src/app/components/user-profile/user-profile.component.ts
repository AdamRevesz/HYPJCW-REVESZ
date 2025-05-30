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
  userId = '2811c75d-ae64-44f4-90f7-32aa02bd2202';
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
          email: '',
          password: '',
          professional: this.user.isProfessional || false
        });

        this.userLoadError = null;
      },
      error: (err) => {
        console.log(`Error loading in user ID: ${this.userId}`, err);
        this.userLoadError = 'Failed to load user information';
      }
    })
  }
}
