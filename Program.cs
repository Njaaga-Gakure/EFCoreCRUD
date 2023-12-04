using Products.Controllers;
using Products.Data;

using ProductContext context = new ProductContext();

ProductController productController = new ProductController();

productController.Index(context);