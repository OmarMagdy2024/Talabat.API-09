using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.ServicesContract
{
    public interface IcashService
    {
        public Task CashAsync(string cashkey, object response,TimeSpan expiretime);
        public Task<string> GetCashAsync(string cashkey);
    }
}
