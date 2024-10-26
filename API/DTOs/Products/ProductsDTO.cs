using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs.Category;

namespace API.DTOs.Products
{
    public class ProductsDTO
    {
        public int Id { get; set; }
        public string? ProductName { get; set; }
        public int? Quantity { get; set; }
        public int? Price { get; set; }
        public int? GrossAmount { get; set; }
    }
}