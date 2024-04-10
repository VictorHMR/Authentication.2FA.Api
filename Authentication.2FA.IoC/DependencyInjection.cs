using Authentication._2FA.Infrastructure.Context;
using Authentication._2FA.Ioc.UseCases;
using Authentication._2FA.IoC.Repositories;
using Authentication._2FA.IoC.Validator;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Authentication._2FA.Ioc
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Authentication_2FA");
            services.AddDbContext<Authentication_2FAContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)), ServiceLifetime.Transient);

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddRepositories(configuration);
            services.AddValidator(configuration);
            services.AddUseCases(configuration);

            return services;
        }
    }
}