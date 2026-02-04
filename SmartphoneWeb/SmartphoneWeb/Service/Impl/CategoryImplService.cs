using SmartphoneWeb.Models;
using Microsoft.EntityFrameworkCore;
namespace SmartphoneWeb.Service.Impl
{
    public class CategoryImplService : CategoryService
    {
        private readonly AppDbContext _context;

        public CategoryImplService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            return await _context.Categories.FirstOrDefaultAsync(m => m.CategoryId == id);
        }

        public async Task AddCategoryAsync(Category category)
        {
            if (category.CategoryDate == null)
            {
                category.CategoryDate = DateTime.Now;
            }
            _context.Add(category);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            _context.Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }
        }

        public bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.CategoryId == id);
        }
    }


}
