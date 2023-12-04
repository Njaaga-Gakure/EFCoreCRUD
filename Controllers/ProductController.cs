using Products.Data;
using Products.Models;
using Products.Services;
using System.Numerics;

namespace Products.Controllers
{
    public class ProductController
    {

        ProductService productService = new ProductService();
        public void Index(ProductContext context) {
            do {
                Console.WriteLine("\t\tWelcome User");
                Console.WriteLine("\t\t-------------\n");
                Console.WriteLine("Press `1` to create a products.");
                Console.WriteLine("Press `2` to view all products.");
                Console.WriteLine("Press `3` to get single product by Id");
                Console.WriteLine("Press `4` to update a product");
                Console.WriteLine("Press `5` to delete a product");
                string? option = Console.ReadLine();
                var validOptions =  new List<string>() { "1", "2", "3", "4", "5"}; 
                try
                {
                    ValidateOptions(option, validOptions);
                }
                catch (Exception e) {
                    Console.WriteLine(e.Message);
                    continue;
                }
                int choice = int.Parse(option);
                RedirectUser(choice, context);
            } while (true);    
        }

        public void RedirectUser(int choice, ProductContext context) {
                switch(choice) {
                case 1:
                    AddProduct(context);
                    break;
                case 2:
                    ListProducts(context);
                    break;
                case 3:
                    GetSingleProduct(context);
                    break;
                case 4:
                    UpdateProduct(context);
                    break;
                case 5:
                    DeleteProduct(context);
                    break;
            }   
        }
        public void ValidateOptions(string option, List<string> validOptions) {
            if (!validOptions.Contains(option))
                throw new Exception("\t\t------------ invalid option -------------- \n");
        }
        public void AddProduct(ProductContext context) {
            do {
                Console.WriteLine("Add a Product");
                Console.WriteLine("Enter a Name");
                string? name = Console.ReadLine();  
                Console.WriteLine("Enter a Description");
                string? description = Console.ReadLine();
                Console.WriteLine("Enter a Price");
                string? price = Console.ReadLine();
                var inputs = new List<string>() { name, description, price};
                try
                {
                    CheckEmptyOrNullValues(inputs);
                    PriceIsDecimal(price);
                }
                catch (Exception e) {
                    Console.WriteLine(e.Message);
                    continue;
                }

                decimal p = decimal.Parse(price);

                var product = new Product() { Name=name, Description=description, Price=p};
                bool isSuccessful = productService.CreateProduct(product, context);
                Console.WriteLine(isSuccessful ? "---- product created successfully ---- \n" : "----- something went wrong ----- \n");
                break;
            } while (true);
        }

        public void ListProducts(ProductContext context) {
            var products = productService.GetProducts(context);

            Console.WriteLine("\n\tProducts");
            Console.WriteLine("\t----------\n");
           foreach (var product in products) {
                Console.WriteLine($"{product.Id}) {product.Name} : {product.Price}");
            }
            Console.WriteLine();

        }

        public void GetSingleProduct(ProductContext context) {
            do {
                Console.WriteLine("\nProduct");
                Console.WriteLine("----------\n");
                Console.WriteLine("Enter Product Id");
                string? option = Console.ReadLine();
                int productId;
                try
                {
                    productId = ValidInteger(option);
                }
                catch (Exception e) {
                    Console.WriteLine(e.Message);
                    continue;
                }
                var product = productService.GetProductById(productId, context);
                if (product != null)
                {
                    Console.WriteLine("\nProduct");
                    Console.WriteLine("-------\n");
                    Console.WriteLine($"{product.Id}) {product.Name} : {product.Price}\n");
                }
                else {
                    Console.WriteLine("------- No Product Found ------\n");
                }
                break;
            } while(true);

        }

        public void UpdateProduct(ProductContext context) {
            do
            {
                Console.WriteLine("Update a Product");
                Console.WriteLine("----------------\n");
                Console.WriteLine("Enter Id of the Product You Want To Update");
                string? option = Console.ReadLine();
                int productId;
                try
                {
                    productId = ValidInteger(option);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }
                Console.WriteLine("Enter New Name");
                string? name = Console.ReadLine();
                Console.WriteLine("Enter New Description");
                string? description = Console.ReadLine();
                Console.WriteLine("Enter New Price");
                string? price = Console.ReadLine();
                var inputs = new List<string>() { name, description, price };
                try
                {
                    CheckEmptyOrNullValues(inputs);
                    PriceIsDecimal(price);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }

                decimal p = decimal.Parse(price);


                bool isSuccessful = productService.UpdateProduct(productId, name, description, p, context);
                Console.WriteLine(isSuccessful ? "---- product updated successfully ---- \n" : "----- something went wrong ----- \n");
                break;
            } while (true);

        }

        public void DeleteProduct(ProductContext context) {
            do
            {
                Console.WriteLine("\nProduct");
                Console.WriteLine("----------\n");
                Console.WriteLine("Enter Id of the Product You Want To Delete");
                string? option = Console.ReadLine();
                int productId;
                try
                {
                    productId = ValidInteger(option);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }
                bool isSuccessful = productService.DeleteProduct(productId, context);
                Console.WriteLine(isSuccessful? "------ Product deleted successfully ------\n" : "--------- Something went wrong ---------\n");
                break;
            } while (true);
        }
        public void CheckEmptyOrNullValues(List<string> inputs) {
            if (inputs.Contains(null) || inputs.Contains(""))
                throw new Exception("\t\t------ price fill in all the fields -------\n");
        }
        public void PriceIsDecimal(string price) {
            if (!decimal.TryParse(price, out decimal p))
                throw new Exception("\t\t------- Please enter a valid price value -------- \n");
        }

        public int ValidInteger(string option) {
            bool isValidInt = int.TryParse(option, out int id);
            if (!isValidInt)
                throw new Exception(" ------ id must be an integer ---------\n");
            return id;
        }
    }
}
