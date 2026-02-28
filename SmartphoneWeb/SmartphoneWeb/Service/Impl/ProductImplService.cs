using Microsoft.EntityFrameworkCore;
using SmartphoneWeb.Models;
using SmartphoneWeb.Service;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartphoneWeb.Service.Impl
{
    public class ProductImplService : ProductService
    {
        private readonly AppDbContext _context;

        public ProductImplService(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            // Dùng Include để lấy thông tin Category tương ứng với Product
            return _context.Products.Include(p => p.Category).ToList();
        }

        public IEnumerable<Product> SearchProductsByName(string keyword)
        {
            if (string.IsNullOrEmpty(keyword))
            {
                return GetAllProducts();
            }
            return _context.Products
                           .Include(p => p.Category)
                           .Where(p => p.ProductName.Contains(keyword))
                           .ToList();
        }

        public Product GetProductById(int id)
        {
            return _context.Products.Include(p => p.Category).FirstOrDefault(p => p.ProductId == id);
        }

        public void CreateProduct(Product product)
        {
            product.ProductDate = DateTime.Now; // Tự động gán ngày tạo
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void UpdateProduct(Product product)
        {
            var existingProduct = _context.Products.Find(product.ProductId);
            if (existingProduct != null)
            {
                existingProduct.ProductName = product.ProductName;
                existingProduct.Descriptions = product.Descriptions;
                existingProduct.Price = product.Price;
                existingProduct.ImageUrl = product.ImageUrl;
                existingProduct.Quantity = product.Quantity;
                existingProduct.CategoryId = product.CategoryId;
                // Giữ nguyên ProductDate gốc hoặc cập nhật tùy logic của bạn

                _context.Products.Update(existingProduct);
                _context.SaveChanges();
            }
        }

        public void DeleteProduct(int id)
        {
            var product = _context.Products.Find(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
        }
    }
}