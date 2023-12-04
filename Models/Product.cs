using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Products.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public string? Description { get; set; }

        [Column(TypeName = "decimal(8,2)")]
        public decimal Price { get; set; }  
    }
}
