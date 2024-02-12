using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebAppPrac.Models.Domain;
using WebAppPrac.Models.DTO;
using WebAppPrac.Repositories.Interface;

namespace WebAppPrac.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostsController : ControllerBase
    {
        private readonly IBlogPostRepository blogPostRepository;
        private readonly ICategoryRepository categoryRepository;

        public BlogPostsController(IBlogPostRepository blogPostRepository, ICategoryRepository categoryRepository)
        {
            this.blogPostRepository = blogPostRepository;
            this.categoryRepository = categoryRepository;
        }
        [HttpPost]
        public async Task<IActionResult> CreateBlogPost(CreateBlogPostRequestDTO request)
        {
            var blogPost = new BlogPost
            {
                Title = request.Title,
                Content = request.Content,
                ImageUrl = request.ImageUrl,
                IsVisible = request.IsVisible,
                ShortDescription = request.ShortDescription,
                Author = request.Author,
                CreatedAt = request.CreatedAt,
                UrlHandle = request.UrlHandle,
                Categories = new List<Category>()
            };

            foreach (var categoryGuid in request.Categories)
            {
                var existingCategory = await categoryRepository.GetById(categoryGuid);

                if (existingCategory != null)
                {
                    blogPost.Categories.Add(existingCategory);
                }
            }

            blogPost = await blogPostRepository.CreateAsync(blogPost);

            var response = new BlogPostDto
            {
                Id = blogPost.Id,
                Title = blogPost.Title,
                Content = blogPost.Content,
                ImageUrl = blogPost.ImageUrl,
                IsVisible = blogPost.IsVisible,
                ShortDescription = blogPost.ShortDescription,
                Author = blogPost.Author,
                CreatedAt = blogPost.CreatedAt,
                UrlHandle = blogPost.UrlHandle,
                Categories = blogPost.Categories.Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle,
                }).ToList(),
            };

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBlogPost()
        {
            var blogPosts = await blogPostRepository.GetAllAsync();

            var response = new List<BlogPostDto>();

            foreach (var blogPost in blogPosts)
            {
                response.Add(new BlogPostDto
                {
                    Id = blogPost.Id,
                    Title = blogPost.Title,
                    Content = blogPost.Content,
                    ImageUrl = blogPost.ImageUrl,
                    IsVisible = blogPost.IsVisible,
                    ShortDescription = blogPost.ShortDescription,
                    Author = blogPost.Author,
                    CreatedAt = blogPost.CreatedAt,
                    UrlHandle = blogPost.UrlHandle,
                    Categories = blogPost.Categories.Select(x => new CategoryDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                        UrlHandle = x.UrlHandle,
                    }).ToList(),
                });
            }

            return Ok(response);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetBlogPostById(Guid id)
        {
            var existingBlogPost = await blogPostRepository.GetById(id);

            if (existingBlogPost == null)
            {
                return NotFound();
            }

            var response = new BlogPostDto
            {
                Id = existingBlogPost.Id,
                Title = existingBlogPost.Title,
                Content = existingBlogPost.Content,
                ImageUrl = existingBlogPost.ImageUrl,
                IsVisible = existingBlogPost.IsVisible,
                ShortDescription = existingBlogPost.ShortDescription,
                Author = existingBlogPost.Author,
                CreatedAt = existingBlogPost.CreatedAt,
                UrlHandle = existingBlogPost.UrlHandle,
                Categories = existingBlogPost.Categories.Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle,
                }).ToList()
            };

            return Ok(response);
        }


        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateBlogPost(Guid id, UpdateBlogPostRequestDto request)
        {
            var blogpost = new BlogPost
            {
                Id = id,
                Title = request.Title,
                Content = request.Content,
                ImageUrl = request.ImageUrl,
                IsVisible = request.IsVisible,
                ShortDescription = request.ShortDescription,
                Author = request.Author,
                CreatedAt = request.CreatedAt,
                UrlHandle = request.UrlHandle,
                Categories = new List<Category>()
            };

            foreach (var categoryGuid in request.Categories)
            {
                var existingCategory = await categoryRepository.GetById(categoryGuid);

                if (existingCategory != null)
                {
                    blogpost.Categories.Add(existingCategory);
                }
            }

            var updatedBlogpost = await blogPostRepository.UpdateAsync(blogpost);

            if (updatedBlogpost == null)
            {
                return NotFound();
            }

            var response = new BlogPostDto
            {
                Id = blogpost.Id,
                Title = blogpost.Title,
                Content = blogpost.Content,
                ImageUrl = blogpost.ImageUrl,
                IsVisible = blogpost.IsVisible,
                ShortDescription = blogpost.ShortDescription,
                Author = blogpost.Author,
                CreatedAt = blogpost.CreatedAt,
                UrlHandle = blogpost.UrlHandle,
                Categories = blogpost.Categories.Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle,
                }).ToList()
            };


            return Ok(response);

        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteBlogPost(Guid id)
        {
            var blogPost = await blogPostRepository.DeleteAsync(id);

            if (blogPost == null)
            {
                return NotFound();
            }

            var response = new BlogPostDto
            {
                Id = blogPost.Id,
                Title = blogPost.Title,
                Content = blogPost.Content,
                ImageUrl = blogPost.ImageUrl,
                IsVisible = blogPost.IsVisible,
                ShortDescription = blogPost.ShortDescription,
                Author = blogPost.Author,
                CreatedAt = blogPost.CreatedAt,
                UrlHandle = blogPost.UrlHandle,
            };

            return Ok(response);
        }
    }
}
