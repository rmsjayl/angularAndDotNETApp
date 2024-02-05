import { Component, OnInit } from '@angular/core';
import { AddBlogpost } from '../models/add-blogpost.model';
import { BlogPostService } from '../services/blog-post.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-blogpost',
  templateUrl: './add-blogpost.component.html',
  styleUrls: ['./add-blogpost.component.sass'],
})
export class AddBlogpostComponent implements OnInit {
  model: AddBlogpost;

  constructor(
    private blogPostService: BlogPostService,
    private router: Router
  ) {
    this.model = {
      title: '',
      shortDescription: '',
      content: '',
      imageUrl: '',
      urlHandle: '',
      author: '',
      createdAt: new Date(),
      isVisible: true,
    };
  }

  ngOnInit(): void {}

  onFormSubmit(): void {
    this.blogPostService.createBlogPost(this.model).subscribe({
      next: (response) => {
        this.router.navigateByUrl('/admin/blogposts');
      },
      error: (error) => {
        console.error('Failed to create blog post!', error);
      },
    });
  }
}
