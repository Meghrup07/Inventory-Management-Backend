using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs.Category
{
    public class UpdateCategoryDTO
    {
        [Required]
        public required string CategoryType { get; set; }
        [Required]
        public required string CategoryName { get; set; }
    }
}