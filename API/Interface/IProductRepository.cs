using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs.Products;
using API.Helpers;
using API.Models;

namespace API.Interface
{
    public interface IProductRepository
    {
        Task<PagedList<ProductsDTO>> GetAsync(UserParams userParams);
        Task<Products> GetByIdAsync(int id);
        Task CreateAsync(Products products);
        Task UpdateAsync(Products products);
        Task DeleteAsync(Products products);
        Task<bool> SaveChangesAsync();
    }
}