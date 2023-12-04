using Products.Data;
using Products.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.Services.IServices
{
    public interface IProduct
    {
        bool CreateProduct(Product product, ProductContext context);
        List<Product> GetProducts(ProductContext context);
        Product? GetProductById(int id, ProductContext context);

        bool UpdateProduct(int id, string name, string description, decimal price, ProductContext context);

        bool DeleteProduct(int id, ProductContext context);
    }
}
