using Code.Models.Domain;
using Code.Models.DTO;
using Code.Repositories.Interface;
using Code_Pulse_API.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace Code.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        public readonly ICategoryRepository _categoryRepository;
        public CategoriesController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryRequestDTO request)
        {
            var category = new Category();
            category.Name = request.Name;
            category.UrlHandle = request.UrlHandle;

            await _categoryRepository.CreateAsync(category);

            var response = new CategoryDTO { 
                 Id = category.Id,
                 Name = request.Name,
                 UrlHandle = request.UrlHandle
            };

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        { 
           var categories = await _categoryRepository.GetAllAsync();
           
            var response = new List<CategoryDTO>();
            foreach (var category in categories) 
            {

                response.Add(new CategoryDTO
                {
                    Id = category.Id,
                    Name = category.Name,
                    UrlHandle = category.UrlHandle
                });
            }
            
            return Ok(response);
        }

        [HttpGet]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> GetCategoryByID(Guid Id)
        { 
           var existingCategory = await _categoryRepository.GetById(Id);

            if (existingCategory == null) 
            {
                return NotFound();
            }

            var response = new CategoryDTO
            {
                Id = existingCategory.Id,
                Name = existingCategory.Name,
                UrlHandle = existingCategory.UrlHandle
            };
            return Ok(response);
        }
    }
}
