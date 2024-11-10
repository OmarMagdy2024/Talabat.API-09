
using AutoMapper;
using Talabat.API.Helpers;
using Talabat.API.ModelDTO;
using Talabat.Core.Models;
using Talabat.Core.Models.Order;

namespace Talabat.API.Profiless
{
    public class Profiles:Profile
    {
        public Profiles()
        {
            CreateMap<Product,ProductDTO>()
                .ForMember(s=>s.ProductBrand,d=>d.MapFrom(p=>p.ProductBrand.Name))
                .ForMember(s => s.ProductType, d => d.MapFrom(p => p.ProductType.Name))
                .ForMember(s=>s.PictureUrl,d=>d.MapFrom<PictureUrlSolver>());

            CreateMap<Basket, BasketDTO>().ReverseMap();
            CreateMap<BasketItem, BasketItemDTO>().ReverseMap();
            CreateMap<AppUser,UserDTO>().ReverseMap();
            CreateMap<AppUser,RegisterDTO>().ReverseMap();

            CreateMap<Address, AddressDTO>().ReverseMap();
            //CreateMap<Order, ReturnOrderDTO>().ReverseMap()
            //    .ForMember(o => o.DeliveryType.Cost, d => d.MapFrom(p => p.DeliveryCost))
            //    .ForMember(o => o.DeliveryType.ShortName, d => d.MapFrom(p => p.DeliveryName));
                //.ForMember(o => o.Status.ToString(), d => d.MapFrom(p => p.Status));

        }
    }
}
