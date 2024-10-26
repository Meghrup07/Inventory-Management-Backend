using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs.Customer;
using API.Helpers;
using API.Interface;
using API.Models;
using API.Response;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController(DataContext _context, ICustomerRepository customerRepository, IMapper mapper) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(CustomerDTO), StatusCodes.Status200OK)]
        public async Task<ActionResult<PaginationResponse<CustomerDTO>>> GetCustomers([FromQuery] UserParams userParams)
        {

            var customers = await customerRepository.GetAsync(userParams);

            var response = new PaginationResponse<CustomerDTO>
            {
                Items = customers,
                TotalCount = customers.Count
            };

            return Ok(response);

        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(CustomerDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Customer>> GetCustomerById([FromRoute] int id)
        {
            var customer = await customerRepository.GetByIdAsync(id);

            if (customer == null) return NotFound();

            return Ok(customer);
        }


        [HttpPost]
        [ProducesResponseType(typeof(CustomerDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Customer>> CreateCustomer(AddCustomerDTO addCustomerDTO)
        {

            if (await CustEmailExits(addCustomerDTO.Email)) return BadRequest("Email already taken!");

            var customer = mapper.Map<Customer>(addCustomerDTO);

            await customerRepository.CreateAsync(customer);

            await customerRepository.SaveChangesAsync();

            return Ok(customer);

        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(CustomerDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Customer>> UpdateCustomer([FromRoute] int id, UpdateCustomerDTO updateCustomerDTO)
        {
            var customer = await customerRepository.GetByIdAsync(id);

            if (customer == null) return NotFound();

            mapper.Map(updateCustomerDTO, customer);

            await customerRepository.UpdateAsync(customer);
            await customerRepository.SaveChangesAsync();

            return Ok(customer);

        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(typeof(CustomerDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Customer>> DeleteCustomer([FromRoute] int id)
        {
            var customer = await customerRepository.GetByIdAsync(id);

            if (customer == null) return NotFound();

            await customerRepository.DeleteAsync(customer);
            await customerRepository.SaveChangesAsync();

            return Ok(customer);
        }

        private async Task<bool> CustEmailExits(string email)
        {
            return await _context.Customers.AnyAsync(e => e.Email == email);
        }
    }
}