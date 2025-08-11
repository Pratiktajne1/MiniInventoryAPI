using Microsoft.AspNetCore.Mvc;
using MiniInventoryAPI.Model;
using MiniInventoryAPI.Services;

namespace MiniInventoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : Controller
    {
        private readonly IAppServices _appServices;

        public HomeController(IAppServices appServices)
        {
            _appServices = appServices;
        }


        [HttpPost("categories")]
        public async Task<IActionResult> CreateCategory(Category category)
        {
            var result = await _appServices.CreateCategory(category);
            return CreatedAtAction(nameof(GetCategoryById), new { id = result.Id }, result);
        }

        [HttpGet("categories/{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var result = await _appServices.GetCategoriesById(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpGet("categories/search")]
        public async Task<IActionResult> GetCategoriesByName([FromQuery] string name)
        {
            var result = await _appServices.GetCategoriesByName(name);
            return Ok(result);
        }

        [HttpPut("categories/{id}")]
        public async Task<IActionResult> UpdateCategory(int id, Category category)
        {
            if (id != category.Id) return BadRequest("ID mismatch");
            var result = await _appServices.UpdateCategoriesById(category);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpDelete("categories/{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var result = await _appServices.DeleteCategorieasById(id);
            if (result == null) 
                return NotFound();
            return Ok(result);
        }

        [HttpPost("products")]
        public async Task<IActionResult> CreateProduct(Product product)
        {
            var result = await _appServices.CreateProduct(product);
            return CreatedAtAction(nameof(GetProductById), new { id = result.Id }, result);
        }

        [HttpGet("products/{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var result = await _appServices.GetProductsById(id);
            if (result == null) 
                return NotFound();
            return Ok(result);
        }

        [HttpGet("products")]
        public async Task<IActionResult> GetProducts(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] int? categoryId = null,
            [FromQuery] string? search = null,
            [FromQuery] string? sortBy = "name",
            [FromQuery] string sortDirection = "asc")
        {
            var result = await _appServices.GetProducts(pageNumber, pageSize, categoryId, search, sortBy, sortDirection);
            return Ok(result);
        }

        [HttpPut("products/{id}")]
        public async Task<IActionResult> UpdateProduct(int id, Product product)
        {
            if (id != product.Id) return BadRequest("ID mismatch");
            var result = await _appServices.UpdateProductsById(product);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpDelete("products/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await _appServices.DeleteProductsById(id);
            if (result == null) return NotFound();
            return Ok(result);
        }
    }
}
