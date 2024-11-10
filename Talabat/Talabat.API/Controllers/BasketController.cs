using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Talabat.API.Errors;
using Talabat.API.ModelDTO;
using Talabat.Core.Interfaces;
using Talabat.Core.Models;
using Talabat.Repository.Repositories;

namespace Talabat.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IRedisRepository _basketRepository;
        private readonly IMapper _map;

        public BasketController(IRedisRepository basketRepository,IMapper map)
        {
            _basketRepository = basketRepository;
            _map = map;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Basket>> GetBasket(string id)
        {
            return Ok(await _basketRepository.GetBasketAsync(id)??new Basket(id)); 
        }

        [HttpPost]
        public async Task<ActionResult<Basket>> UpdateBasket(BasketDTO basket)
        {
            var mapbasket = _map.Map<BasketDTO, Basket>(basket);
            return Ok(await _basketRepository.AddOrUpdateBasketAsync(mapbasket) is null?BadRequest(new APIResponse(400)): await _basketRepository.AddOrUpdateBasketAsync(mapbasket));
        }
        [HttpDelete]
        public async Task<bool> DeleteBasket(string id)
        {
            return await _basketRepository.DeleteBasketAsync(id);
        }
    }
}
