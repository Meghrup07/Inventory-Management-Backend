using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class AppUser
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public required string UserName { get; set; }
        [Required]
        public required string Name { get; set; }
        [Required]
        public required string Email { get; set; }
        public string? Phone { get; set; }
        [Required]
        public required byte[] passwordHash { get; set; }
        [Required]
        public required byte[] passwordSalt { get; set; }
    }
}