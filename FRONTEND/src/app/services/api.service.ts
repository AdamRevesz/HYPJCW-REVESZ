import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, Subject, tap, map } from 'rxjs';
import {
  UserShortViewDto,
  ContentShortViewDto,
  VideoShortViewDto,
  PictureShortViewDto,
  SalesItemShortViewDto,
  CourseShortViewDto,
  CommentViewDto,
  ContentCreateDto,
  PictureCreateDto,
  CommentCreateDto,
  ContentViewDto,
  UserViewDto
} from '../models/content.model'
import { observableToBeFn } from 'rxjs/internal/testing/TestScheduler';
import { Form } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private backendUrl = 'https://localhost:7024';

  private contentAddedSource = new Subject<void>();
  contentAdded$ = this.contentAddedSource.asObservable();


  constructor(private http: HttpClient) { }

  getComment(id: string): Observable<CommentViewDto[]> {
    return this.http.get<CommentViewDto[]>(`${this.backendUrl}/api/${id}`)
  }

  addCommentToContent(contentId: string, commentData: CommentCreateDto): Observable<void> {
    return this.http.post<void>(`${this.backendUrl}/api/addcomment?contentId=${contentId}`, commentData);
  }

  deleteComment(commentId: string): Observable<void> {
    return this.http.delete<void>(`${this.backendUrl}/api/Comment/deletecomment/${commentId}`);
  }

  getContent(): Observable<ContentShortViewDto[]> {
    return this.http.get<ContentShortViewDto[]>(`${this.backendUrl}/api/Content`);
  }

  getContentById(id: string): Observable<ContentViewDto> {
    return this.http.get<ContentViewDto>(`${this.backendUrl}/api/Content/getcontent/${id}`);
  }

  addContent(contentData: ContentCreateDto): Observable<void> {
    return this.http.post<void>(`${this.backendUrl}/api/Content`, contentData);
  }

  deleteContent(id: string): Observable<void> {
    return this.http.delete<void>(`${this.backendUrl}/api/Content/contentdelete/${id}`);
  }

  //picture api calls
  addPicture(pictureData: FormData): Observable<void> {
    return this.http.post<void>(`${this.backendUrl}/addpicture`, pictureData).pipe(
      tap(() => {
        this.contentAddedSource.next();
      })
    );
  }

  deletePicture(id: string): Observable<void> {
    console.log(`ApiService: deletePicture called for ID: ${id}`);
    return this.http.delete<void>(`${this.backendUrl}/api/Content/contentDelete/${id}`).pipe(
      tap(() => {
        console.log(`ApiService: deletePicture HTTP call successful for ID: ${id}. Emitting contentAddedSource.next()`);
        this.contentAddedSource.next();
      })
    );
  }

  editPicture(id: string, pictureData: FormData): Observable<void> {
    console.log(`ApiService: editPicture called for ID:${id}`);
    return this.http.put<void>(`${this.backendUrl}/updatepicture/${id}`, pictureData).pipe(
      tap(() => {
        this.notifyContentAdded();
      })
    );
  }

  notifyContentAdded() {
    this.contentAddedSource.next();
  }
  //Course api calls
  addCourse(courseData: FormData): Observable<void> {
    return this.http.post<void>(`${this.backendUrl}/api/Course/addcourse`, courseData).pipe(
      tap(() => {
        this.notifyContentAdded();
      })
    );
  }

  getCourses(): Observable<CourseShortViewDto> {
    return this.http.get<CourseShortViewDto>(`${this.backendUrl}/api/Content`)
  }

  //Sales Item Api calls
  addSalesItem(salesItemData: FormData): Observable<void> {
    return this.http.post<void>(`${this.backendUrl}/api/SalesItem/addsalesitem`, salesItemData).pipe(
      tap(() => {
        this.notifyContentAdded();
      })
    );
  }

  getSalesItems(): Observable<SalesItemShortViewDto[]> {
    return this.http.get<SalesItemShortViewDto[]>(`${this.backendUrl}/api/SalesItem`);
  }

  getSalesItemById(id: string): Observable<SalesItemShortViewDto> {
    return this.http.get<SalesItemShortViewDto>(`${this.backendUrl}/getsalesitem/${id}`);
  }

  editSalesItem(id: string, salesItem: FormData): Observable<void> {
    return this.http.put<void>(`${this.backendUrl}/updatesalesitem/${id}`, salesItem).pipe(
      tap(() => {
        this.notifyContentAdded();
      })
    )
  }

  //User Api call
  getUser(id: string): Observable<UserViewDto> {
    return this.http.get<UserViewDto[]>(`${this.backendUrl}/api/User/getuser${id}`).pipe(
      tap((users) => console.log('API Response:', users)),
      map((users: UserViewDto[]) => users[0])
    );
  }

  updateUserPicture(id: string, userData: FormData): Observable<void> {
    return this.http.put<void>(`${this.backendUrl}/api/User/uploadpicture/${id}`, userData).pipe(
      tap(() => {
        this.notifyContentAdded();
      })
    )
  }

  updateUser(id: string, userData: FormData): Observable<void> {
    return this.http.put<void>(`${this.backendUrl}/api/User/updateuser/${id}`, userData).pipe(
      tap(() => {
        this.notifyContentAdded();
      })
    )
  }
  //Like Content Api call
  likeContent(id: string, userId: string): Observable<void> {
    return this.http.post<void>(`${this.backendUrl}/api/Rate/${id}/like?userId=${userId}`, {}).pipe(
      tap(() => {
        this.notifyContentAdded();
      })
    );
  }

  //Dislike Content Api call
  dislikeContent(id: string, userId: string): Observable<void> {
    return this.http.post<void>(`${this.backendUrl}/api/Rate/${id}/dislike?userId=${userId}`, {}).pipe(
      tap(() => {
        this.notifyContentAdded();
      })
    );
  }
}