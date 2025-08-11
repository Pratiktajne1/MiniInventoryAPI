using MiniInventoryAPI.Model;

namespace MiniInventoryAPI.Services
{
    public interface IAppServices
    {
        public  Task<IEnumerable<Category>> GetCategoriesByName(string? name);
        public Task<Category?> GetCategoriesById(int id);
        public Task<Category?> UpdateCategoriesById(Category category);
        public Task<Category?> DeleteCategorieasById(int id);
        public Task<Product?> GetProductsById(int id);
        public Task<Product?> UpdateProductsById(Product product);
        public Task<Product?> DeleteProductsById(int id);
        public Task<Product> CreateProduct(Product product);
        public Task<Category> CreateCategory(Category category);
        public Task<IEnumerable<Product>> GetProducts(int pageNumber = 1,int pageSize = 10, int? categoryId = null, string? search = null, string? sortBy = "name",string sortDirection = "asc");
    }
}
