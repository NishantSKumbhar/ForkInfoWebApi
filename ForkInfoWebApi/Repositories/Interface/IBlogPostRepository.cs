using ForkInfoWebApi.Models.Domain;

namespace ForkInfoWebApi.Repositories.Interface
{
    public interface IBlogPostRepository
    {
        Task<BlogPost> CreateAsync(BlogPost blogPost);
    }
}
