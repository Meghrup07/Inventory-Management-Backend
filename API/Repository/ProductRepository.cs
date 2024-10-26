using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs.Products;
using API.Helpers;
using API.Interface;
using API.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class ProductRepository(DataContext context, IMapper mapper) : IProductRepository
    {
        public async Task CreateAsync(Products products)
        {
            await context.Products.AddAsync(products);
        }

        public async Task DeleteAsync(Products products)
        {
            context.Products.Remove(products);
        }

        public async Task<PagedList<ProductsDTO>> GetAsync(UserParams userParams)
        {
            var query = context.Products.AsQueryable();

            if (userParams.Search != null)
            {
                query = query.Where(s => s.ProductName.ToLower().Contains(userParams.Search.ToLower()));
            }
            return await PagedList<ProductsDTO>.CreateAsync(query.ProjectTo<ProductsDTO>(mapper.ConfigurationProvider), userParams.PageNumber, userParams.PageSize);
        }

        public async Task<Products> GetByIdAsync(int id)
        {
            return await context.Products.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await context.SaveChangesAsync() > 0;
        }

        public async Task UpdateAsync(Products products)
        {
            context.Entry(products).State = EntityState.Modified;
        }
    }
}