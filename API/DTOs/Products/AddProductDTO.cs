using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs.Products
{
    public class AddProductDTO
    {
        [Required]
        public required string ProductName { get; set; }
        [Required]
        public required int Quantity { get; set; }
        [Required]
        public required int Price { get; set; }
        [Required]
        public required int GrossAmount { get; set; }
        [Required]
        public int CategoryId { get; set; }
    }
}