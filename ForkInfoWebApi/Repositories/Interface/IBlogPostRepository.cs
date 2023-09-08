using ForkInfoWebApi.Models.Domain;

namespace ForkInfoWebApi.Repositories.Interface
{
    public interface IBlogPostRepository
    {
        Task<BlogPost> CreateAsync(BlogPost blogPost);
        Task<BlogPost?> GetByIdAsync(Guid id);
        Task<BlogPost?> GetByUrlHandleAsync(string urlHandle);
        Task<IEnumerable<BlogPost>> GetBlogPosts();
        Task<BlogPost?> UpdateAsync(Guid id, BlogPost blogPost);
        Task<BlogPost?> DeleteAsync(Guid id);
    }
}
