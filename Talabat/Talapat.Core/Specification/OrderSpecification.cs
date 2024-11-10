using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Models;
using Talabat.API.Helpers;
using Talabat.Core.Models.Order;
namespace Talabat.Core.Specification
{
    public class OrderSpecification : BaseSpecification<Order>
    {
        public OrderSpecification(OrderParams proparams) : base
            (p =>
            (!string.IsNullOrEmpty(proparams.Email) || p.CustomerEmail == proparams.Email)
            &&
            (!proparams.Id.HasValue || p.Id == proparams.Id)
            &&
            (string.IsNullOrEmpty(proparams.PaymentIntentId)||p.PaymentIntentId==proparams.PaymentIntentId)
            )

        {
            Includes.Add(p => p.CustomerInformation);
            Includes.Add(p => p.DeliveryType);
            Includes.Add(p => p.OrderItems);
            AddOrderBy(p => p.DateTime);

            if (proparams.Ispagination == true)
            {
                applypagination((proparams.pageindex - 1) * proparams.PageSize, proparams.PageSize);
            }
        }
    }     
}
