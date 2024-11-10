using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.API.Errors;
using Talabat.API.ModelDTO;
using Talabat.Core.Interfaces;

namespace Talabat.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly IMapper _mapper;

        public PaymentController(IPaymentService paymentService,IMapper mapper)
        {
            _paymentService = paymentService;
            _mapper = mapper;
        }
        [ProducesResponseType(typeof(BasketDTO),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIResponse), StatusCodes.Status400BadRequest)]
        [HttpPost("{basketid}")]
        public async Task<ActionResult<BasketDTO>> CreateOrUpdatePaymentIntent(string basketid)
        {
            var basket = await _paymentService.CreateOrUpdatePaymentIntent(basketid);
            if (basket == null) return BadRequest(new APIResponse(400));
            var mappedbasket = _mapper.Map<BasketDTO>(basket);
            return Ok(mappedbasket);
        }
    }
}
