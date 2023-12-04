using Products.Data;
using Products.Models;
using Products.Services.IServices;


namespace Products.Services
{
    public class ProductService : IProduct
    {
        public bool CreateProduct(Product product, ProductContext context)
        {
            try
            {
                context.Products.Add(product);
                context.SaveChanges();
                return true;
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        public List<Product> GetProducts(ProductContext context)
        {
            try { 
                var products = context.Products.OrderBy(product => product.Name);
                return products.ToList();
            } catch (Exception e) {
                Console.WriteLine(e.Message);
                return new List<Product>();
            }
        }
        public Product? GetProductById(int id, ProductContext context)
        {
            try {
                var product = context.Products.Where(product => product.Id == id).FirstOrDefault();

                return product != null ? product : null;
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public bool UpdateProduct(int id, string name, string description, decimal price, ProductContext context)
        {
            try
            {
                var product = context.Products.Where(product => product.Id == id).FirstOrDefault();
                if (product is Product) { 
                    product.Name = name;
                    product.Description = description;
                    product.Price = price;
                    context.SaveChanges();
                    return true;
                }

                return false;
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        public bool DeleteProduct(int id, ProductContext context)
        {
            try
            {
                var product = context.Products.Where(product => product.Id == id).FirstOrDefault();
                if (product is Product) { 
                    context.Products.Remove(product);
                    context.SaveChanges();
                    return true;
                }
               return false;    
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
       
    }
}
