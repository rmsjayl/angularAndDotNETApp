﻿namespace WebAppPrac.Models.DTO
{
    public class BlogPostDto
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? ShortDescription { get; set; }
        public string? Content { get; set; }
        public string? ImageUrl { get; set; }
        public string? UrlHandle { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? Author { get; set; }
        public bool? IsVisible { get; set; }
        public List<CategoryDto> Categories { get; set; } = new List<CategoryDto>();

    }
}
