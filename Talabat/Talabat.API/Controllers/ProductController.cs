using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Talabat.API.Errors;
using Talabat.API.Helpers;
using Talabat.API.ModelDTO;
using Talabat.Core.Interfaces;
using Talabat.Core.Models;
using Talabat.Core.Specification;

namespace Talabat.API.Controllers
{
	[Route("Talabat/[controller]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private readonly IGenaricRepository<Product> _product;
        private readonly IGenaricRepository<ProductBrand> _prand;
        private readonly IGenaricRepository<ProductType> _type;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public ProductController(IGenaricRepository<Product> product,IGenaricRepository<ProductBrand> prand,IGenaricRepository<ProductType> type,IMapper mapper,IConfiguration configuration)
        {
			_product = product;
            _prand = prand;
            _type = type;
            _mapper = mapper;
            _configuration = configuration;
        }

		[CashAttribute(300)]
		[HttpGet]
		[Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
		public async Task<ActionResult<IReadOnlyList<Product>>> GetAllProduct([FromQuery]ProductParams proparams)
		{
			var spec=new ProductSpecification(proparams);
			var product = await _product.GetAllWithSpecAsync(spec);
			if (product == null)
			{
				return NotFound(new APIResponse(404));
			}
			var data=_mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductDTO>>(product);
			var count = _product.GetCount();

            return Ok(new Pagination<ProductDTO>(proparams.PageSize,proparams.pageindex,count,data));
		}

		[ProducesResponseType(typeof(ProductDTO),StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(APIResponse), StatusCodes.Status404NotFound)]
		[HttpGet("{id}")]
		public async Task<ActionResult<Product>> GetProductById(int id,string? sort)
        {
            var spec = new ProductSpecification(id,sort);
			var product = await _product.GetByIdWithSpecAsync(spec);

            if (product == null)
            {
                return NotFound(new APIResponse(404));
            }
            return Ok(new ProductDTO()
			{
				Id=product.Id,
				Name=product.Name,
				PictureUrl = $"{_configuration["BaseURL"]}/{product.PictureUrl}",
				Price=product.Price,
				ProductBrand=product.ProductBrand.Name,
				ProductType=product.ProductType.Name,
				Description=product.Description,
			});

		}
		[HttpGet("Prand")]
		public async Task<ActionResult<IEnumerable<ProductBrand>>> GetPrand()
		{
			var prand = await _prand.GetAllAsync();
			if (prand.Count() > 0)
			{
				return Ok(await _prand.GetAllAsync());
			}
			return NotFound(new APIResponse(404));
		}

		[HttpGet("Catepory")]
		public async Task<ActionResult<IEnumerable<ProductType>>> GetCategory()
		{
			var type = await _type.GetAllAsync();
			if (type.Count() > 0)
			{
				return Ok(await _type.GetAllAsync());
			}
			return NotFound(new APIResponse(404));
		}
	}
}
