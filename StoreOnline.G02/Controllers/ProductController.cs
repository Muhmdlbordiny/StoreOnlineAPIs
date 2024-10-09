using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreCore.G02.Dto.Products;
using StoreCore.G02.Helper;
using StoreCore.G02.RepositriesContract;
using StoreCore.G02.Specifications.Products;

namespace StoreOnline.G02.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet] //Get baseurl/api/product
        public async Task< IActionResult> GetAllProduct([FromQuery]Productspecparms productspec )
        {
           var result=await _productService.GetAllProductAsync( productspec);
            return Ok(result);//200
        }
        [HttpGet("brands")]
        public async Task< IActionResult> GetAllBrands()
        {
           var result = await _productService.GetAllBrandsAsync(); 
            return Ok(result);
        }
        [HttpGet("types")]
        public async Task<IActionResult> GetAllTypes()
        {
            var result = await _productService.GetAllTypeAsync();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int?id)
        {
            if (id is null) return BadRequest("Invaild !!");
            var result = await _productService.GetProductById(id.Value);
            if (result is null) return NotFound($"The product with Id : {id} not found"); 
            return Ok(result);
        }
    }
}
