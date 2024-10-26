using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public required string CategoryType { get; set; }
        [Required]
        public required string CategoryName { get; set; }
        public List<Products> Products { get; set; } = new();
    }
}