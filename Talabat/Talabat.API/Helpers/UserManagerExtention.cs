using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Talabat.Core.Models;

namespace Talabat.API.Helpers
{
    public static class UserManagerExtention
    {
        public static async Task<AppUser> FindAddressByEmailAsync(this UserManager<AppUser> userManager,ClaimsPrincipal user)
        {
            var email = user.FindFirstValue(ClaimTypes.Email);
            var User = userManager.Users.Include(u => u.Address).FirstOrDefault(u => u.NormalizedEmail == email.ToUpper());
            return User;
        }
    }
}
