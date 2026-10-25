using Ecommerce.SharedLirary.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Application.DTOs;
using ProductApi.Application.DTOs.Mappers;
using ProductApi.Application.Interfaces;

namespace ProductApi.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IProduct productInterface) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> getProduct()
        {
            var products = await productInterface.GetAllAsync();
            if (!products.Any())
                return NotFound("No products detected in the database");
            var (_, list) = ProductMapper.FromEntity(null!, products);
            return list!.Any() ? Ok(list) : NotFound("No products detected in the database");
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductDto>> getProductById(int id)
        {
            var product = await productInterface.FindByIdAsync(id);
            if (product == null)
                return NotFound("Product requested not found");
            var (_product, _) = ProductMapper.FromEntity(product, null);
            return _product is not null ? Ok(_product) : NotFound("Product  not found");
        }

        [HttpPost]
        public async Task<ActionResult<Response>> CreateProduct([FromBody] ProductDto productDto)
        {

            // checking if model state is valid
            if (!ModelState.IsValid)
                return BadRequest("Invalid product data provided");
            // convert to entity and save to database
            var product = ProductMapper.ToEntity(productDto);
            var response = await productInterface.CreateAsync(product);
            return response.Flag ? Ok(response) : BadRequest(response);
        }

        [HttpPut]
        public async Task<ActionResult<Response>> UpdateProduct([FromBody] ProductDto productDto)
        {
            // checking if model state is valid
            if (!ModelState.IsValid)
                return BadRequest("Invalid product data provided");

            // convert to entity and update in database
            var product = ProductMapper.ToEntity(productDto);
            var response = await productInterface.UpdateAsync(product);
            return response.Flag ? Ok(response) : BadRequest(response);
        }

        [HttpDelete]
        public async Task<ActionResult<Response>> DeleteProduct([FromBody] ProductDto productDto)
        {
            // convert to entity and delete from database
            var product = ProductMapper.ToEntity(productDto);
            var response = await productInterface.DeleteAsync(product);
            return response.Flag ? Ok(response) : BadRequest(response);
        }
    }
}
