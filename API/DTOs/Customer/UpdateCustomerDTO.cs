using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs.Customer
{
    public class UpdateCustomerDTO
    {
        [Required]
        public required string Name { get; set; }
        [Required]
        public required string Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        [Required]
        public required string State { get; set; }
        [Required]
        public required string City { get; set; }
    }
}