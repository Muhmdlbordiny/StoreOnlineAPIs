using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreCore.G02.Dto.Products;
using StoreCore.G02.Helper;
using StoreCore.G02.RepositriesContract;
using StoreCore.G02.Specifications.Products;
using StoreOnline.G02.Error;

namespace StoreOnline.G02.Controllers
{
    
    public class ProductController : BaseApiController
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [ProducesResponseType(typeof(PaginationResponse<ProductDto>),StatusCodes.Status200OK)]
        [HttpGet] //Get baseurl/api/product
        public async Task< ActionResult<PaginationResponse<ProductDto>>> GetAllProduct([FromQuery]Productspecparms productspec )
        {
           var result=await _productService.GetAllProductAsync( productspec);
            return Ok(result);//200
        }
        [ProducesResponseType(typeof(IEnumerable<TypeBrandDto>), StatusCodes.Status200OK)]

        [HttpGet("brands")]
        public async Task< ActionResult<IEnumerable<TypeBrandDto>>> GetAllBrands()
        {
           var result = await _productService.GetAllBrandsAsync(); 
            return Ok(result);
        }
        [ProducesResponseType(typeof(IEnumerable<TypeBrandDto>), StatusCodes.Status200OK)]

        [HttpGet("types")]
        public async Task<ActionResult<IEnumerable<TypeBrandDto>>> GetAllTypes()
        {
            var result = await _productService.GetAllTypeAsync();
            return Ok(result);
        }
        [ProducesResponseType(typeof(IEnumerable<ProductDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<ApiErrorResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IEnumerable<ApiErrorResponse>), StatusCodes.Status404NotFound)]

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int?id)
        {
            if (id is null) return BadRequest(new ApiErrorResponse(400));
            var result = await _productService.GetProductById(id.Value);
            if (result is null) return NotFound
                    (new ApiErrorResponse(404, $"The product with Id : {id} not found")); 
            return Ok(result);
        }
    }
}
