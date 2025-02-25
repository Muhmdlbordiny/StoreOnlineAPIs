﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreCore.G02.Dto.DeliveryMethods;
using StoreCore.G02.Dto.orders;
using StoreCore.G02.Entites.Orders;
using StoreCore.G02.RepositriesContract;
using StoreCore.G02.ServiceContract;
using StoreOnline.G02.Error;
using System.Security.Claims;

namespace StoreOnline.G02.Controllers
{
    public class OrderController : BaseApiController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public OrderController(IOrderService orderService,IMapper mapper,IUnitOfWork unitOfWork)
        {
            _orderService = orderService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        [Authorize]
        [HttpPost("CreateOrder")]
        public async Task<IActionResult> CreateOrder(OrderDto model)
        {
          var useremail =  User.FindFirstValue(ClaimTypes.Email);
            if (useremail is null)
                return Unauthorized(new ApiErrorResponse(StatusCodes.Status401Unauthorized));
           var address = _mapper.Map<Address>(model.ShipToAddress);
           var order = await _orderService.CreateOrderAsync(useremail,model.BasketId,model.DeliveryMethodId , address);
            if (order is null)
                return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
            return Ok(_mapper.Map<OrderToReturnDto>(order));
        }

        [Authorize]
        [HttpGet]
        public async Task <IActionResult> GetOrdersForSpecificUser()
        {
            var useremail = User.FindFirstValue(ClaimTypes.Email);
            if (useremail is null)
                return Unauthorized(new ApiErrorResponse(StatusCodes.Status401Unauthorized));
          var orders = await  _orderService.GetOrderForSpecificUserAsync(useremail);
            if (orders is null)
                return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
            return Ok(_mapper.Map<IEnumerable<OrderToReturnDto>>(orders));
        }
        [Authorize]
        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrdersForSpecificUser(int? orderId)
        {
            if (orderId is null)
                return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
            var useremail = User.FindFirstValue(ClaimTypes.Email);
            if (useremail is null)
                return Unauthorized(new ApiErrorResponse(StatusCodes.Status401Unauthorized));
            var order = await _orderService.GetOrderByIdForSpecificUserAsync(useremail,orderId.Value);
            if (order is null)
                return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound));
            return Ok(_mapper.Map<OrderToReturnDto>(order));
        }
        [HttpGet("GetAllDeleveryMethods")]
        public async Task <IActionResult> GetDeleveryMethods()
        {
          var deliverymethod = await  _unitOfWork.Repositry<DeliveryMethod,int>().GetAllAsync();
            if (deliverymethod is null)
                return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
            return Ok(_mapper.Map<IEnumerable<DeliveryMethodDto>>(deliverymethod));
        }

    }
}
