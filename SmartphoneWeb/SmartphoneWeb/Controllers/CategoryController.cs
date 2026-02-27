using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartphoneWeb.Models;
using SmartphoneWeb.Service;

namespace SmartphoneWeb.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly CategoryService categoryService;

        public CategoryController(CategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        // GET: Category
        public async Task<IActionResult> Index()
        {
            var categories = await categoryService.GetAllCategoriesAsync();
            return View(categories);
        }

        // GET: Category/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var category = await categoryService.GetCategoryByIdAsync(id.Value);
            if (category == null) return NotFound();

            return View(category);
        }

        // GET: Category/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Category/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryName")] Category category)
        {
            // Chỉ bind CategoryName, CategoryDate sẽ tự động tạo
            if (ModelState.IsValid)
            {
                await categoryService.AddCategoryAsync(category);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Category/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var category = await categoryService.GetCategoryByIdAsync(id.Value);
            if (category == null) return NotFound();

            return View(category);
        }

        // POST: Category/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryId,CategoryName,CategoryDate")] Category category)
        {
            if (id != category.CategoryId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    await categoryService.UpdateCategoryAsync(category);
                }
                catch (Exception)
                {
                    if (!categoryService.CategoryExists(category.CategoryId))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Category/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var category = await categoryService.GetCategoryByIdAsync(id.Value);
            if (category == null) return NotFound();

            return View(category);
        }

        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await categoryService.DeleteCategoryAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}