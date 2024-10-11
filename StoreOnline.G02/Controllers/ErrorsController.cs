using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreOnline.G02.Error;

namespace StoreOnline.G02.Controllers
{
    [Route("error")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi =true)]
    public class ErrorsController : BaseApiController
    {
        public IActionResult Error(int code)
        {
            return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound,"Not Found !!"));
        }
    }
}
