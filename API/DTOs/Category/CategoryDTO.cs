using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs.Products;

namespace API.DTOs.Category
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string? CategoryType { get; set; }
        public string? CategoryName { get; set; }
    }
}