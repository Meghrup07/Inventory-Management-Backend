using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs.Customer;
using API.DTOs.Products;
using API.DTOs.Users;
using API.Models;
using AutoMapper;

namespace API.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Entity to DTO
            CreateMap<Customer, CustomerDTO>();
            CreateMap<Products, ProductsDTO>();

            // DTO to Entity
            CreateMap<RegisterDTO, AppUser>();
            CreateMap<AddCustomerDTO, Customer>();
            CreateMap<UpdateCustomerDTO, Customer>();
            CreateMap<AddProductDTO, Products>();
            CreateMap<UpdateProductDTO, Products>();
        }

    }
}