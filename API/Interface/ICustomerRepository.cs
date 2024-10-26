using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs.Customer;
using API.Helpers;
using API.Models;

namespace API.Interface
{
    public interface ICustomerRepository
    {
        Task<PagedList<CustomerDTO>> GetAsync(UserParams userParams);
        Task<Customer> GetByIdAsync(int id);
        Task CreateAsync(Customer customer);
        Task UpdateAsync(Customer customer);
        Task DeleteAsync(Customer customer);
        Task<bool> SaveChangesAsync();
    }
}