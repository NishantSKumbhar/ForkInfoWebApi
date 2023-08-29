using ForkInfoWebApi.Models.Domain;
using ForkInfoWebApi.Models.DTO;
using ForkInfoWebApi.Repositories.Implementation;
using ForkInfoWebApi.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ForkInfoWebApi.Controllers
{
    [Route("api/[controller]")]
    //[AllowAnonymous]
    [ApiController]
    public class BlogPostController : ControllerBase
    {
        private readonly IBlogPostRepository blogPostRepository;
        private readonly ICategoryRepository categoryRepository;

        public BlogPostController(IBlogPostRepository blogPostRepository, ICategoryRepository categoryRepository)
        {
            this.blogPostRepository = blogPostRepository;
            this.categoryRepository = categoryRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBlogPost(BlogPostGetDTO request)
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
                PublishedDate= request.PublishedDate,
                Categories = new List<Category>()
            };

            foreach (var categoryGuid in request.Categories)
            {
                var existingCategory = await categoryRepository.GetById(categoryGuid);
                if(existingCategory is not null)
                {
                    blog.Categories.Add(existingCategory);
                }
            }

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
                Author = result.Author,
                Categories = result.Categories.Select(x => new CategorySendDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle= x.UrlHandle
                }).ToList()
            };
            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<List<BlogPostSendDTO>>> GetBlogPosts()
        {
            var blogs = await blogPostRepository.GetBlogPosts();

            var blogpostSend = new List<BlogPostSendDTO>();
            foreach (var blog in blogs)
            {
                blogpostSend.Add(new BlogPostSendDTO
                {
                    Id = blog.Id,
                    Title = blog.Title,
                    UrlHandle = blog.UrlHandle,
                    ShortDescription = blog.ShortDescription,
                    PublishedDate = blog.PublishedDate,
                    FeaturedImageUrl = blog.FeaturedImageUrl,
                    IsVisible = blog.IsVisible,
                    Content = blog.Content,
                    Categories = blog.Categories.Select(x => new CategorySendDTO
                    {
                        Id = x.Id,
                        Name = x.Name,
                        UrlHandle = x.UrlHandle
                    }).ToList()
                });
            }

            return Ok(blogpostSend);
        }
    }
}
