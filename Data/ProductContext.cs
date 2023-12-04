using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Products.Models;

namespace Products.Data
{
    public class ProductContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var configSection = configBuilder.GetSection("ConnectionStrings");
            var connectionString = configSection["SQLServerConnection"] ?? null;
            optionsBuilder.UseSqlServer(connectionString);
            // optionsBuilder.UseSqlServer("Server=DESKTOP-0G9EJP0;Database=E-COMMERCE_STORE;Trusted_Connection=True;TrustServerCertificate=True;");
        }
        public DbSet<Product> Products {get; set; }
    }
}
