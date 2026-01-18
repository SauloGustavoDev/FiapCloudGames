using Domain.Entities;
using Infrastructure.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;

namespace Api.Configurations
{
    public static class ContextConfiguration
    {
        public static void AddContextConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ContextDb>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
);

            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireUppercase = false; // ajuste se quiser
            })
                .AddEntityFrameworkStores<ContextDb>()
                .AddDefaultTokenProviders();
        }
    }
}
