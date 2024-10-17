using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using StoreCore.G02.Dto.Basket;
using StoreCore.G02.Entites;
using StoreCore.G02.RepositriesContract;
using StoreOnline.G02.Error;

namespace StoreOnline.G02.Controllers
{
    
    public class BasketController : BaseApiController
    {
        private readonly IBasketService _basketservice;
        private readonly IMapper _mapper;

        public BasketController(IBasketService basketservice,IMapper mapper)
        {
            _basketservice = basketservice;
            _mapper = mapper;
        }
        [HttpGet("{id}")]
        public async Task <ActionResult<CustomerBasket>> GetBasketById(string ?id)
        {
            if (id is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest,"Invaild Id"));
           var basket = await _basketservice.GetBasketAsync(id);
            //if(basket is null)
            //{
            //    new CustomerBasket()
            //    {
            //        Id = id
            //    };
            //}
            if (basket is null)
                return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound));
            return Ok(basket);

        }
        [HttpPost]
        public async Task< ActionResult<CustomerBasket>>CreateOrUpdatedBasket(CustomerBasketDto? model)
        {
            if (model is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
            var basket =  await _basketservice.UpdateBasketAsync(model);
            if (basket is null) return BadRequest(new ApiErrorResponse(400));
            return Ok(basket);
        }
        [HttpDelete]
        public async Task<ActionResult> DeleteBasket(string? id)
        {
            if(id is null)
                return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
            var flag = await _basketservice.DeleteBasketAsync(id);
            if (flag is false)
                return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
            return NoContent();


        }


    }
}
