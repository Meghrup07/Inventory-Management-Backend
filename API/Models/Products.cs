using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Products
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public required string ProductName { get; set; }
        [Required]
        public required int Quantity { get; set; }
        [Required]
        public required int Price { get; set; }
        [Required]
        public required int GrossAmount { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
    }
}