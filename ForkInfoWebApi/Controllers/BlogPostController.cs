using ForkInfoWebApi.Models.Domain;
using ForkInfoWebApi.Models.DTO;
using ForkInfoWebApi.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ForkInfoWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostController : ControllerBase
    {
        private readonly IBlogPostRepository blogPostRepository;

        public BlogPostController(IBlogPostRepository blogPostRepository)
        {
            this.blogPostRepository = blogPostRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBlogPost([FromBody] BlogPostGetDTO request)
        {
            var blog = new BlogPost
            {
                Title = request.Title,
                Author= request.Author,
                FeaturedImageUrl= request.FeaturedImageUrl,
                Content = request.Content,
                IsVisible = request.IsVisible,
                UrlHandle = request.UrlHandle,
                ShortDescription= request.ShortDescription,
                PublishedDate= request.PublishedDate
            };

            var result = await this.blogPostRepository.CreateAsync(blog);

            var response = new BlogPostSendDTO
            {
                Id = result.Id,
                Title = result.Title,
                Content= result.Content,
                ShortDescription = result.ShortDescription,
                PublishedDate= result.PublishedDate,
                FeaturedImageUrl = result.FeaturedImageUrl,
                IsVisible= result.IsVisible,
                UrlHandle = result.UrlHandle,
                Author = result.Author
            };
            return Ok(response);
        }
    }
}
