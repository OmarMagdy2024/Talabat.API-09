using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Models;
using Talabat.Repository.Connections;

namespace Talabat.Repository.DataSeeding
{
    public static class IdentityDataSeeding
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if(userManager.Users.Count()== 0)
            {
                var user = new AppUser()
                {
                    DisplayName="omar magdy",
                    Email="omar.mego17@gmail.com",
                    UserName="omarmagdy",
                    PhoneNumber="01152265672"
                };
                await userManager.CreateAsync(user,"P@ssw0rd");
            }
        }
    }
}
