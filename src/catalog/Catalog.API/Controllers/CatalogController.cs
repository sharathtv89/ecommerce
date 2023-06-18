using System.Net;
using Catalog.API.Entities;
using Catalog.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CatalogController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<CatalogController> _logger;
        public CatalogController(IProductRepository productRepository, ILogger<CatalogController> logger)
        {            
            _productRepository = productRepository;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts(){
            var products = await _productRepository.GetProducts();
            return Ok(products);
        }

        [HttpGet("{id}", Name = "GetProductById")]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductById(string id){
            var product = await _productRepository.GetProductById(id);
            if(product == null){
                _logger.LogError("Product not found by id : {0}", id);
                return NotFound();
            }
            return Ok(product);
        }

        [HttpGet("GetProductByName/{name}", Name = "GetProductByName")]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductByName(string name){
            var products = await _productRepository.GetProductByName(name);
            if(products == null){
                _logger.LogError("Product not found by name : {0}", name);
                return NotFound();
            }
            return Ok(products);
        }

        [HttpGet("GetProductByCategory/{category}", Name = "GetProductByCategory")]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductByCategory(string category){
            var product = await _productRepository.GetProductByCategory(category);
            if(product == null){
                _logger.LogError("Product not found by category : {0}", category);
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost("", Name = "CreateProduct")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
        {            
            var result = await _productRepository.CreateProduct(product);
            if(result == null)
            {
                _logger.LogError("There is an error occurred while saving your product details, please contact support team");
                return StatusCode(500);
            }

            return Ok(product);
        }

        [HttpPut("", Name = "UpdateProduct")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> UpdateProduct([FromBody] Product product)
        {
            var result = await _productRepository.UpdateProduct(product);
            if(result == null)
            {
                _logger.LogError("There is an error occurred while saving your product details, please contact support team");
                return StatusCode(500);
            }

            return Ok(product);
        }

        [HttpPut("{id}", Name = "DeleteProduct")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> DeleteProduct(string id)
        {
            var result = await _productRepository.DeleteProduct(id);
            if(!result)
            {
                _logger.LogError("There is an error occurred while deleting the product, please contact support team");
                return StatusCode(500);
            }

            return Ok();
        }
    }
}