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

        [HttpGet]
        public async Task<ActionResult<List<CategorySendDTO>>> GetAllCategories()
        {
            var categories = await categoryRepository.GetAllAsync();

            var categoriesSend = new List<CategorySendDTO>();
            foreach (var category in categories)
            {
                categoriesSend.Add(new CategorySendDTO
                {
                    Id = category.Id,
                    Name = category.Name,
                    UrlHandle = category.UrlHandle
                });
            }

            return Ok(categoriesSend);
        }

        [HttpGet("{id}")]
        
        public async Task<ActionResult<CategorySendDTO>> GetCategoryById([FromRoute] Guid id)
        {
            var category = await categoryRepository.GetById(id);
            if(category is null)
            {
                return NotFound();
            }
            var response = new CategorySendDTO { Id = category.Id,Name = category.Name, UrlHandle= category.UrlHandle };
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory([FromRoute] Guid id, CategoryGetDTO categoryGet)
        {
            var category = new Category
            {
                Id = id,
                Name = categoryGet.Name,
                UrlHandle = categoryGet.UrlHandle
            };

            category = await categoryRepository.UpdateAsync(category);

            if(category != null)
            {
                var response = new CategorySendDTO
                {
                    Name = category.Name,
                    UrlHandle = category.UrlHandle
                };
                return Ok(response);
            }
            return NotFound();
        }

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

        [HttpDelete("{id}")]
        public async Task<ActionResult<CategorySendDTO>> DeleteCategory([FromRoute] Guid id)
        {
           var category = await categoryRepository.DeleteAsync(id);
            if(category != null )
            {

                var response = new CategorySendDTO
                {
                    Name = category.Name,
                    UrlHandle = category.UrlHandle
                };

                return Ok(response);
            }

            return NotFound();
        }
    }
}
