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

        public async Task<BlogPost?> GetByIdAsync(Guid id)
        {
            return await this.applicationDbContext.BlogPosts.Include(x => x.Categories).FirstOrDefaultAsync(x => x.Id == id);
            
        }

        public async Task<BlogPost?> UpdateAsync(Guid id, BlogPost blogPost)
        {
            var blog = await this.applicationDbContext.BlogPosts.Include(x => x.Categories).FirstOrDefaultAsync(x => x.Id == id);
            if(blog == null)
            {
                return null;
            }
            applicationDbContext.Entry(blog).CurrentValues.SetValues(blogPost);
            blog.Categories = blogPost.Categories;
            await applicationDbContext.SaveChangesAsync();
            return blog;
        }
    }
}
