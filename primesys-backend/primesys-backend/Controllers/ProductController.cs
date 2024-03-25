using Microsoft.AspNetCore.Mvc;
using primesys_backend.Interfaces;
using primesys_backend.Models;

namespace primesys_backend.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost("CreateNewProduct")]
        public async Task<ActionResult<ResultModel<bool>>> CreateMessage([FromBody] ProductCreateModel product)
        {
            var response = await _productService.CreateProduct(product);

            return Ok(response);
        }

        [HttpGet("GetAllProduct")]
        public async Task<ActionResult<ResultModel<List<ProductBaseModel>>>> GetAllProduct()
        {
            var response = await _productService.GetAllProduct();

            return Ok(response);
        }

        [HttpDelete("DeleteProduct/{id}")]
        public async Task<ActionResult<ResultModel<bool>>> DeleteProductById(int id)
        {
            var response = await _productService.DeleteProduct(id);

            return Ok(response);
        }

        [HttpGet("GetProductById/{id}")]
        public async Task<ActionResult<ResultModel<ProductBaseModel>>> GetProductById(int id)
        {
            var response = await _productService.GetProductById(id);

            return Ok(response);
        }

        [HttpPut("UpdateProduct")]
        public async Task<ActionResult<ResultModel<bool>>> UpdateProduct([FromBody] ProductBaseModel product)
        {
            var response = await _productService.UpdateProduct(product);

            return Ok(response);
        }

    }
}
