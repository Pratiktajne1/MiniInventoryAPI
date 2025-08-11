using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using MiniInventoryAPI.Model;
using MiniInventoryAPI.Services;

namespace MiniInventoryAPI.Services
{
    public class AppServices : IAppServices
    {
        private readonly AppDbContext _dbContext;
        public AppServices(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Category?> DeleteCategorieasById(int id)
        {
            var result = await this.GetCategoriesById(id);
            if (result != null)
            {
                _dbContext.Categories.Remove(result);
                await _dbContext.SaveChangesAsync();
                return result;
            }
            return null;
        }
        public async Task<IEnumerable<Product>> GetProducts(int pageNumber = 1, int pageSize = 10, int? categoryId = null, string? search = null, string? sortBy = "name", string sortDirection = "asc")
        {
            IQueryable<Product> query = _dbContext.Products.Include(p => p.Category);

            if (categoryId.HasValue)
                query = query.Where(p => p.CategoryId == categoryId.Value);

            if (!string.IsNullOrWhiteSpace(search))
            {
                string lowerSearch = search.ToLower();
                query = query.Where(p => p.Name.ToLower() == search.ToLower() || p.Sku.ToLower() == search.ToLower());
            }

            sortBy = sortBy?.ToLower() ?? "name";
            sortDirection = sortDirection?.ToLower() ?? "asc";

            query = sortBy switch
            {
                "price" => (sortDirection == "desc") ? query.OrderByDescending(p => p.Price) : query.OrderBy(p => p.Price),

                _ => (sortDirection == "desc") ? query.OrderByDescending(p => p.Name) : query.OrderBy(p => p.Name),
            };

            query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            return await query.ToListAsync();
        }

        public async Task<Category> CreateCategory(Category category)
        {
            _dbContext.Categories.Add(category);
            await _dbContext.SaveChangesAsync();
            return category;
        }

        public async Task<Product> CreateProduct(Product product)
        {
            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();
            return product;
        }

        public async Task<Product?> DeleteProductsById(int id)
        {
           var result = await this.GetProductsById(id);
            if (result != null)
            {
                _dbContext.Products.Remove(result);
                await _dbContext.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<Category?> GetCategoriesById(int id)
        {
            var result = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);

            if (result != null)
                return result;
            else
                return null;

        }

        public async Task<IEnumerable<Category>> GetCategoriesByName(string? name)
        {
            var result = await _dbContext.Categories.Where(c => c.Name.ToLower() == name.ToLower()).ToListAsync();
            if (result != null)
                return result;
            else
                return Enumerable.Empty<Category>();
        }

        public async Task<Product?> GetProductsById(int id)
        {
            var result = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);

            if (result != null)
                return result;
            else
                return null;
        }

        public async Task<Category?> UpdateCategoriesById(Category category)
        {
           var result = await this.GetCategoriesById(category.Id);
            if (result == null)
            {
                return null;
            }
            result.Id = category.Id;
            result.Name = category.Name;
            result.Description = category.Description;
            await _dbContext.SaveChangesAsync();
            return result;
        }

        public async Task<Product> UpdateProductsById(Product product)
        {
            var result = await this.GetProductsById(product.Id);
            if (result == null)
            {
                return null;
            }
            result.Id = product.Id;
            result.Name = product.Name;
            result.Sku = product.Sku;
            result.Price = product.Price;
            result.Category = product.Category;
            result.RowVersion = product.RowVersion;
            result.QuantityInStock = product.QuantityInStock;
            await _dbContext.SaveChangesAsync();
            return result;
        }
    }
}
