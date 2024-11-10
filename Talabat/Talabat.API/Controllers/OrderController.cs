using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Talabat.API.Errors;
using Talabat.API.ModelDTO;
using Talabat.Core.Models.Order;
using Talabat.Core.ServicesContract;

namespace Talabat.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderServices _orderServices;
        private readonly IMapper _mapper;

        public OrderController(IOrderServices orderServices,IMapper mapper)
        {
            _orderServices = orderServices;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(OrderDTO orderDTO)
        {
            var result = await _orderServices.CreateOrderAsync(orderDTO.email, orderDTO.basketid, orderDTO.deliveryid, new CustomerInformation()
            {
                City = orderDTO.City,
                Country = orderDTO.Country,
                FristName = orderDTO.FristName,
                LastName = orderDTO.LastName,
                Street = orderDTO.Street,
            });
            if (result == null) 
                return BadRequest(new APIResponse(400));
            return Ok(result);
            //return Ok(new ReturnOrderDTO<DeliveryType,CustomerInformation,OrderItem>()
            //{
            //    CustomerEmail = result.CustomerEmail,
            //    CustomerInformation = result.CustomerInformation,
            //    DateTime = result.DateTime,
            //    Delivery = result.DeliveryType,
            //    OrderItems = result.OrderItems,
            //    PayMentId = result.PayMentId,
            //    Status = result.Status
            //});
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Order>>> GetOrderForUser(string emailuser)
        {
            return Ok(await _orderServices.GetOrdersForUserAsync(emailuser));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReturnOrderDTO>> GetOrderForUser(string emailuser,int id)
        {
            var result = await _orderServices.GetOrderForUser(emailuser, id);
            if (result == null)
                return BadRequest(new APIResponse(400));
            var order = new ReturnOrderDTO()
            {
                OrderId = id,
                DateTime = result.DateTime,
                CustomerEmail = emailuser,
                Status = result.Status.ToString(),
                CustomerInformation = result.CustomerInformation,
                DeliveryName = result.DeliveryType.ShortName,
                DeliveryCost = result.DeliveryType.Cost,
                SupTotal = result.SupTotal,
                Total = result.Total,
                OrderItems = result.OrderItems,
            };
            //_mapper.Map<Order, ReturnOrderDTO>(result);
            return Ok(order);
        }
    }
}
