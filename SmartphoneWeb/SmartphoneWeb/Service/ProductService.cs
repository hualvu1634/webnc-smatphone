using SmartphoneWeb.Models;
using System.Collections.Generic;

namespace SmartphoneWeb.Service
{
    public interface ProductService
    {
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> SearchProductsByName(string keyword);
        Product GetProductById(int id);
        void CreateProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(int id);
    }
}