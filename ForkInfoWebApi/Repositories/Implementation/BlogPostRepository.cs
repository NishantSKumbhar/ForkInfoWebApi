using ForkInfoWebApi.Data;
using ForkInfoWebApi.Models.Domain;
using ForkInfoWebApi.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace ForkInfoWebApi.Repositories.Implementation
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly ApplicationDbContext applicationDbContext;

        public BlogPostRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }
        public async Task<BlogPost> CreateAsync(BlogPost blogPost)
        {
            await this.applicationDbContext.BlogPosts.AddAsync(blogPost);
            await this.applicationDbContext.SaveChangesAsync();

            return blogPost;
        }

        public async Task<IEnumerable<BlogPost>> GetBlogPosts()
        {
            return await this.applicationDbContext.BlogPosts.Include(x => x.Categories).ToListAsync();
        }
    }
}
