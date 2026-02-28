using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartphoneWeb.Models;
using SmartphoneWeb.Service;

namespace SmartphoneWeb.Controllers
{
    [Authorize] // Yêu cầu đăng nhập (dựa trên cấu hình Auth trong Program.cs của bạn)
    public class ProductController : Controller
    {
        private readonly ProductService _productService;
        private readonly CategoryService _categoryService;

        public ProductController(ProductService productService, CategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        // Trong ProductController.cs
        public async Task<IActionResult> Index(string keyword)
        {
            // Thêm await ở đây để lấy kết quả thực tế từ Task
            ViewBag.Categories = await _categoryService.GetAllCategoriesAsync();

            IEnumerable<Product> products;
            if (!string.IsNullOrEmpty(keyword))
            {
                products = _productService.SearchProductsByName(keyword);
                ViewBag.Keyword = keyword;
            }
            else
            {
                products = _productService.GetAllProducts();
            }

            return View(products);
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            _productService.CreateProduct(product);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Edit(int id, Product product)
        {
            product.ProductId = id;
            _productService.UpdateProduct(product);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _productService.DeleteProduct(id);
            return RedirectToAction("Index");
        }
    }
}