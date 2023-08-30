using ForkInfoWebApi.Models.Domain;

namespace ForkInfoWebApi.Repositories.Interface
{
    public interface IBlogPostRepository
    {
        Task<BlogPost> CreateAsync(BlogPost blogPost);
        Task<BlogPost?> GetByIdAsync(Guid id);
        Task<IEnumerable<BlogPost>> GetBlogPosts();
        Task<BlogPost?> UpdateAsync(Guid id, BlogPost blogPost);
    }
}
