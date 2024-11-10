﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Models;

namespace Talabat.Core.ServicesContract
{
    public interface IAuthService
    {
        public Task<string> CreateTokenAsync(AppUser user,UserManager<AppUser> manager);
    }
}
