using ForkInfoWebApi.Data;
using ForkInfoWebApi.Models.Domain;
using ForkInfoWebApi.Models.DTO;
using ForkInfoWebApi.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ForkInfoWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoriesController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        //[HttpGet]
        //public async Task<ActionResult<List<CategorySendDTO>>> GetAllCategories()
        //{

        //}

        [HttpPost]
        public async Task<ActionResult<CategorySendDTO>> PostCategory(CategoryGetDTO category)
        {
            var Newcategory = new Category
            {
                Name = category.Name,
                UrlHandle = category.UrlHandle
                
            };

            var RCategory = await categoryRepository.CreateAsync(Newcategory);
            

            var categorySend = new CategorySendDTO
            {
                Id = RCategory.Id,
                Name = RCategory.Name,
                UrlHandle = RCategory.UrlHandle
            };

            return Ok(categorySend);
        }
    }
}
