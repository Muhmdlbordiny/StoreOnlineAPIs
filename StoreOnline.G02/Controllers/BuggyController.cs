using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreOnline.G02.Error;
using StoreRepositry.G02.Data.Contexts;

namespace StoreOnline.G02.Controllers
{
    
    public class BuggyController : BaseApiController
    {
        private readonly StoreDbContext _context;

        public BuggyController(StoreDbContext context)
        {
            _context = context;
        }
        [HttpGet("notfound")]
        public async  Task<IActionResult> GetNotFoundRequestError()
        {
            var brand =await _context.Brands.FindAsync(100);
            if(brand == null)
                return NotFound(new ApiErrorResponse(404));
            return Ok(brand); 
        }
        [HttpGet("servererror")]
        public async Task<IActionResult> GetServerRequestError()
        {
            var brand = await _context.Brands.FindAsync(100);
            var brandtostring = brand.ToString(); // will throw Exception (Null Refernce Exception)
            return Ok(brand);
        }
        [HttpGet("badrequest")]
        public async Task<IActionResult> GetBadRequestError()
        {
            return BadRequest( new ApiErrorResponse(400));
        }
        [HttpGet("badrequest/{id}")]
        public async Task<IActionResult> GetBadRequestError(int id) // vaildation Error
        {
            return Ok();
        }
        [HttpGet("unaithorized")]
        public async Task<IActionResult> GetUnauthorizedRequestError() // vaildation Error
        {
            return Unauthorized(new ApiErrorResponse(401));
        }

    }
}
