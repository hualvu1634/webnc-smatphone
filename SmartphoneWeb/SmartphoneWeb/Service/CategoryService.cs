using SmartphoneWeb.Models;

namespace SmartphoneWeb.Service
{
    public interface CategoryService
    {
        Task<List<Category>> GetAllCategoriesAsync();
        Task<Category?> GetCategoryByIdAsync(int id);
        Task AddCategoryAsync(Category category);
        Task UpdateCategoryAsync(Category category);
        Task DeleteCategoryAsync(int id);
        bool CategoryExists(int id);
    }
}
