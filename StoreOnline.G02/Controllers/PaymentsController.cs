using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreCore.G02.Entites.Orders;
using StoreCore.G02.ServiceContract;
using StoreOnline.G02.Error;
using Stripe;

namespace StoreOnline.G02.Controllers
{
    public class PaymentsController : BaseApiController
    {
        private readonly IPaymentService _paymentService;
        private readonly ILogger<PaymentsController> _logger;
        private const string _webhooksecret = "whsec_2d4bd8afdd722bb19d3e76cac79ca6a23b6495bd0147b642cb20f32dc0a51f88";
        public PaymentsController(IPaymentService paymentService,ILogger<PaymentsController>logger)
        {
            _paymentService = paymentService;
            _logger = logger;
        }
        [HttpPost("{basketId}")]
        [Authorize]
        public async Task<IActionResult> CreatePaymentIntent(string basketId)
        {
            if(basketId is null)
                return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));

            var basket = await  _paymentService.CreateOrUpdatePaymentIntentIdAsync(basketId);
            if (basket is null)
                return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest)); 
           return Ok(basket);
        }
        [HttpPost("webhook")]
        public async Task<IActionResult> StripeWebhook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            
            
                var stripeEvent = EventUtility.ConstructEvent(json, 
                    Request.Headers["Stripe-Signature"], _webhooksecret);
            try
            {
                // Handle the event
                // If on SDK version < 46, use class Events instead of EventTypes
                var paymentintent = stripeEvent.Data.Object as PaymentIntent;
                
                if(stripeEvent.Type== "payment_intent.failed")
                {
                    await _paymentService.UpdatePaymentIntentToSucceededOrFailed(paymentintent.Id, false);

                }
                else if(stripeEvent.Type== " payment_intent.succeeded")
                {
                    await _paymentService.UpdatePaymentIntentToSucceededOrFailed(paymentintent.Id, true);

                }
                else
                {
                    Console.WriteLine("Unhandled Event type :{0}",stripeEvent.Type);
                }

            }catch(StripeException e)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
