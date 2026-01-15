using Application.Services;
using Application.Services.Interfaces;
using Domain.Interfaces;
using Infrastructure.Repository;

namespace Api.Configurations;
    public static class ServicesConfiguration
    {
        public static IServiceCollection AddApplicationServicesConfiguration(this IServiceCollection services)
        {

        #region Repositories
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IGameRepository, GameRepository>();
        #endregion

        #region Services
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IGameService, GameService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<JwtService>();

        #endregion

        return services;
        }
    }


