
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System.Text;
using Talabat.API.Errors;
using Talabat.API.Helpers;
using Talabat.API.MiddleWare;
using Talabat.API.Profiless;
using Talabat.Core.Interfaces;
using Talabat.Core.Models;
using Talabat.Core.ServicesContract;
using Talabat.Repository.Connections;
using Talabat.Repository.DataSeeding;
using Talabat.Repository.Repositories;
using Talabat.Service;

namespace Talabat.API;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        builder.Services.AddApplicationServices();
        builder.Services.AddDbContext<TalabatDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaulfConnection")));
        builder.Services.AddDbContext<IdentityContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection")));
        builder.Services.AddSingleton<IConnectionMultiplexer>((ServiceProvider)=> {
            return ConnectionMultiplexer.Connect(
                builder.Configuration.GetConnectionString("RedisConnection")
            );});
        builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<IdentityContext>();
        builder.Services.AddAuthentication().AddJwtBearer("Bearer", options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidIssuer = builder.Configuration["JWT:ValidIssure"],
                ValidateAudience = true,
                ValidAudience = builder.Configuration["JWT:ValidAudience"],
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:AuthKey"]?? string.Empty))
            };
        });
        builder.Services.AddScoped(typeof(IAuthService),typeof(AuthService));
        var app = builder.Build();
        
        //{
        //    var services = scope.ServiceProvider;
        //    var dbcontext = services.GetRequiredService<TalabatDBContext>();
        //    await dbcontext.Database.MigrateAsync();
        //}
        //finally
        //{
        //    scope.Dispose();
        //}
        using var scope = app.Services.CreateScope();
		var services = scope.ServiceProvider;
		var dbcontext = services.GetRequiredService<TalabatDBContext>();
        var identitydbcontext = services.GetRequiredService<IdentityContext>();
        var usermanger = services.GetRequiredService<UserManager<AppUser>>();
        var loggerfactory=services.GetRequiredService<ILoggerFactory>();
        try
        {
            await dbcontext.Database.MigrateAsync();
            await TalabatContextSeed.SeedAsync(dbcontext);
            await identitydbcontext.Database.MigrateAsync();
            await IdentityDataSeeding.SeedUserAsync(usermanger);
        }
        catch (Exception ex)
        {
            var logger = loggerfactory.CreateLogger<Program>();
            logger.LogError(ex, "An Error Occurred During Migration");
        }
        // Configure the HTTP request pipeline.
        app.UseMiddleware<ExceptionMiddleWare>();
		if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseStatusCodePagesWithReExecute("/Errors/{0}");
        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.UseStaticFiles();

        app.Run();
    }
}
