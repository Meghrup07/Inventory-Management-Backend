using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs.Customer;
using API.Helpers;
using API.Interface;
using API.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class CustomerRepository(DataContext context, IMapper mapper) : ICustomerRepository
    {
        public async Task CreateAsync(Customer customer)
        {
            await context.Customers.AddAsync(customer);
        }

        public async Task DeleteAsync(Customer customer)
        {
            context.Customers.Remove(customer);
        }

        public async Task<PagedList<CustomerDTO>> GetAsync(UserParams userParams)
        {
            var query = context.Customers.AsQueryable();

            if (userParams.Search != null)
            {
                query = query.Where(s => s.Name.ToLower().Contains(userParams.Search.ToLower()));
            }

            return await PagedList<CustomerDTO>.CreateAsync(query.ProjectTo<CustomerDTO>(mapper.ConfigurationProvider), userParams.PageNumber, userParams.PageSize);

        }

        public async Task<Customer> GetByIdAsync(int id)
        {
            return await context.Customers.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await context.SaveChangesAsync() > 0;
        }

        public async Task UpdateAsync(Customer customer)
        {
            context.Entry(customer).State = EntityState.Modified;
        }
    }
}