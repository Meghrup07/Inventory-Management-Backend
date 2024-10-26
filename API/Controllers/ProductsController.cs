using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs.Products;
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
    public class ProductsController(IProductRepository productRepository, DataContext context, IMapper mapper) : ControllerBase
    {

        [HttpGet]
        [ProducesResponseType(typeof(ProductsDTO), StatusCodes.Status200OK)]
        public async Task<ActionResult<PaginationResponse<ProductsDTO>>> GetProducts([FromQuery] UserParams userParams)
        {
            var products = await productRepository.GetAsync(userParams);

            var response = new PaginationResponse<ProductsDTO>
            {
                Items = products,
                TotalCount = products.Count
            };

            return Ok(products);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(ProductsDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Products>> GetProduct([FromRoute] int id)
        {
            var product = await productRepository.GetByIdAsync(id);

            if (product == null) return NotFound();

            return Ok(product);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ProductsDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Products>> CreateProduct(AddProductDTO addProductDTO)
        {
            if (await ProductExists(addProductDTO.ProductName)) return BadRequest("Product name already taken");

            var product = mapper.Map<Products>(addProductDTO);
            product.CategoryId = addProductDTO.CategoryId;

            await productRepository.CreateAsync(product);

            await productRepository.SaveChangesAsync();

            return Ok(product);
        }


        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(ProductsDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Products>> UpdateProduct([FromRoute] int id, UpdateProductDTO updateProductDTO)
        {
            var product = await productRepository.GetByIdAsync(id);

            if (product == null) return NotFound();

            mapper.Map(updateProductDTO, product);

            await productRepository.UpdateAsync(product);

            await productRepository.SaveChangesAsync();

            return Ok(product);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(typeof(ProductsDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Products>> DeleteProduct([FromRoute] int id)
        {
            var product = await productRepository.GetByIdAsync(id);

            if (product == null) return NotFound();

            await productRepository.DeleteAsync(product);

            await productRepository.SaveChangesAsync();

            return Ok(product);
        }

        private async Task<bool> ProductExists(string productName)
        {
            return await context.Products.AnyAsync(p => p.ProductName == productName);
        }

    }
}