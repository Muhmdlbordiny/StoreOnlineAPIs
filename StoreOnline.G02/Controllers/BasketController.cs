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
        private readonly IBasketRepositry _basketRepositry;
        private readonly IMapper _mapper;

        public BasketController(IBasketRepositry basketRepositry,IMapper mapper)
        {
            _basketRepositry = basketRepositry;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task <ActionResult<CustomerBasket>> GetBasket(string ?id)
        {
            if (id is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest,"Invaild Id"));
           var basket = await _basketRepositry.GetBasketAsync(id);
            if(basket is null)
            {
                new CustomerBasket()
                {
                    Id = id
                };
            }
            return Ok(basket);

        }
        [HttpPost]
        public async Task< ActionResult<CustomerBasket>>CreateOrUpdatedBasket(CustomerBasketDto model)
        {
            var basket =  await _basketRepositry.UpdateBasketAsync(_mapper.Map<CustomerBasket>(model));
            if (basket is null) return BadRequest(new ApiErrorResponse(400));
            return Ok(basket);
        }
        [HttpDelete]
        public  Task DeleteBasket(string id)
        {
            return  _basketRepositry.DeleteBasketAsync(id);
        }


    }
}
