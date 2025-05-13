import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
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
} from '../models/content.model'

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private backendUrl = 'https://localhost:7024';
  
  private useMockData = false;

  constructor(private http: HttpClient) { }

  getContent(): Observable<ContentShortViewDto[]>{
    return this.http.get<ContentShortViewDto[]>(`${this.backendUrl}/api/Content`);
  }

  getContentById(id: string): Observable<ContentShortViewDto>{
    return this.http.get<ContentShortViewDto>(`${this.backendUrl}/getcontent/${id}`);
  }

  addContent(contentData: ContentCreateDto): Observable<void>{
    return this.http.post<void>(`${this.backendUrl}/api/Content`, contentData);
  }

  // Update the method signature to match how we're using it
  addPicture(pictureData: FormData): Observable<void> {
    return this.http.post<void>(`${this.backendUrl}/addpicture`, pictureData);
  }
}