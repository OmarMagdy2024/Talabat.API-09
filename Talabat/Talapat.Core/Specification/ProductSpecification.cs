using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Models;
using Talabat.API.Helpers;
namespace Talabat.Core.Specification
{
    public class ProductSpecification:BaseSpecification<Product>
    {
        public ProductSpecification(ProductParams proparams) :base
            (p=>
            (!proparams.brandid.HasValue || p.BrandId==proparams.brandid.Value)
            &&
            (!proparams.categoryid.HasValue || p.TypeId == proparams.categoryid.Value)
            )
        {
            Includes.Add(p=>p.ProductBrand);
            Includes.Add(p=>p.ProductType);
            if (!string.IsNullOrEmpty(proparams.sort))
            {

                switch (proparams.sort)
                {
                    case "name":
                        AddOrderBy(p => p.Name);
                        break;
                    case "price":
                        AddOrderBy(p => p.Price);
                        break;
                    case "namedesc":
                        AddOrderByDescending(p => p.Name);
                        break;
                    case "pricedesc":
                        AddOrderByDescending(p => p.Price);
                        break;
                    default:
                        AddOrderBy(p => p.Name);
                        break;
                };
            }
            else
            {
                AddOrderBy(p => p.Name);
            }
            if (proparams.Ispagination==true)
            {
                applypagination((proparams.pageindex-1)*proparams.PageSize,proparams.PageSize);
            }
        }
        public ProductSpecification(int id, string orderby) : base(p => p.Id == id)
        {
            Includes.Add(p => p.ProductBrand);
            Includes.Add(p => p.ProductType);

            if(!string.IsNullOrEmpty(orderby))
            {

                switch(orderby)
                {
                    case "name":
                        AddOrderBy(p => p.Name);
                        break;
                    case"price": 
                        AddOrderBy(p => p.Price);
                        break;
                    case "namedesc":
                        AddOrderByDescending(p => p.Name);
                        break;
                    case "pricedesc":
                        AddOrderByDescending(p => p.Price);
                        break;
                    default: 
                        AddOrderBy(p => p.Name);
                        break;
                };
            }
        }     
    }
}
