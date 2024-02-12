using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using WebAppPrac.Data;
using WebAppPrac.Models.Domain;
using WebAppPrac.Repositories.Interface;

namespace WebAppPrac.Repositories.Implementation
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly ApplicationDbContext dbContext;

        public BlogPostRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<BlogPost> CreateAsync(BlogPost blogPost)
        {
            await dbContext.BlogPosts.AddAsync(blogPost);
            await dbContext.SaveChangesAsync();
            return blogPost;
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await dbContext.BlogPosts.Include(x => x.Categories).ToListAsync();
        }

        public async Task<BlogPost?> GetById(Guid id)
        {
            return await dbContext.BlogPosts.Include(x => x.Categories).FirstOrDefaultAsync(x => x.Id == id);
        }


        public async Task<BlogPost?> UpdateAsync(BlogPost blogPost)
        {
            var existingBlogPost = await dbContext.BlogPosts.Include(x=> x.Categories).FirstOrDefaultAsync(x => x.Id == blogPost.Id);

            if (existingBlogPost == null)
            {
                return null;
            }

            dbContext.Entry(existingBlogPost).CurrentValues.SetValues(blogPost);
            existingBlogPost.Categories = blogPost.Categories;
            await dbContext.SaveChangesAsync();
            return blogPost;
        }


        public async Task<BlogPost?> DeleteAsync(Guid id)
        {
            var blogpost = await dbContext.BlogPosts.FirstOrDefaultAsync(x => x.Id == id);

            if (blogpost == null)
            {
                return null;
            }

            dbContext.BlogPosts.Remove(blogpost);
            await dbContext.SaveChangesAsync();
            return blogpost;
        }
    }
}
