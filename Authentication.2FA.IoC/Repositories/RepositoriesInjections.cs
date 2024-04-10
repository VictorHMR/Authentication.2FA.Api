using Authentication._2FA.Domain.Interfaces;
using Authentication._2FA.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Authentication._2FA.IoC.Repositories
{
    public static class ServicesInjections
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
