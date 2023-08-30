using Azure.Core;
using ForkInfoWebApi.Models.Domain;
using ForkInfoWebApi.Models.DTO;
using ForkInfoWebApi.Repositories.Implementation;
using ForkInfoWebApi.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;

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

        [HttpGet("{id}")]
        public async Task<ActionResult<BlogPostSendDTO>> GetBlogPostById([FromRoute] Guid id )
        {
            var blogpost = await blogPostRepository.GetByIdAsync(id);
            if(blogpost == null)
            {
                return NotFound();
            }
            var response = new BlogPostSendDTO
            {
                Id=blogpost.Id,
                Title = blogpost.Title,
                UrlHandle = blogpost.UrlHandle,
                ShortDescription = blogpost.ShortDescription,
                PublishedDate = blogpost.PublishedDate,
                FeaturedImageUrl= blogpost.FeaturedImageUrl,
                IsVisible = blogpost.IsVisible,
                Content = blogpost.Content,
                Categories = blogpost.Categories.Select(x => new CategorySendDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle
                }).ToList()
            };

            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBlogPost([FromRoute] Guid id, [FromBody] BlogPostGetDTO request)
        {
            var blogpost = new BlogPost
            {
                Id = id,
                Title = request.Title,
                Author = request.Author,
                FeaturedImageUrl = request.FeaturedImageUrl,
                Content = request.Content,
                IsVisible = request.IsVisible,
                UrlHandle = request.UrlHandle,
                ShortDescription = request.ShortDescription,
                PublishedDate = request.PublishedDate,
                Categories = new List<Category>()
            };

            foreach (var categoryGuid in request.Categories)
            {
                var existingCategory = await categoryRepository.GetById(categoryGuid);
                if(existingCategory != null)
                {
                    blogpost.Categories.Add(existingCategory);
                }

            }
            var updatedBlogPost = await this.blogPostRepository.UpdateAsync(id, blogpost);
            if(updatedBlogPost == null)
            {
                return NotFound();
            }
            var response = new BlogPostSendDTO
            {
                Id = updatedBlogPost.Id,
                Title = updatedBlogPost.Title,
                UrlHandle = updatedBlogPost.UrlHandle,
                ShortDescription = updatedBlogPost.ShortDescription,
                PublishedDate = updatedBlogPost.PublishedDate,
                FeaturedImageUrl = updatedBlogPost.FeaturedImageUrl,
                IsVisible = updatedBlogPost.IsVisible,
                Content = updatedBlogPost.Content,
                Categories = updatedBlogPost.Categories.Select(x => new CategorySendDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle
                }).ToList()
            };
            return Ok(response);
        }
    }
}
