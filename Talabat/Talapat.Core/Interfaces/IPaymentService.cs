using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Models;

namespace Talabat.Core.Interfaces
{
    public interface IPaymentService
    {
        public Task<Basket?> CreateOrUpdatePaymentIntent(string basketid);
    }
}
