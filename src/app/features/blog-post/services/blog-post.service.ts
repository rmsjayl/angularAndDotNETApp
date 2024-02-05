import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { BlogPost } from '../models/blogpost.model';
import { AddBlogpost } from '../models/add-blogpost.model';

@Injectable({
  providedIn: 'root',
})
export class BlogPostService {
  constructor(private http: HttpClient) {}

  createBlogPost(model: AddBlogpost): Observable<BlogPost> {
    return this.http.post<BlogPost>(
      `${environment.baseUrl}/api/BlogPosts`,
      model
    );
  }

  getAllBlogPosts(): Observable<BlogPost[]> {
    return this.http.get<BlogPost[]>(`${environment.baseUrl}/api/BlogPosts`);
  }
}
