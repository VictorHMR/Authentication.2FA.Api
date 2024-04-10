using Authentication._2FA.Application.Interfaces.UseCases;
using Authentication._2FA.Application.UseCases;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Authentication._2FA.Ioc.UseCases
{
    public static class UseCaseInjections
    {
        public static IServiceCollection AddUseCases(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ICreateUserUseCase, CreateUserUseCase>();
            services.AddScoped<IGenerateConfirmationQRUseCase, GenerateConfirmationQRUseCase>();
            services.AddScoped<IUserSigninUseCase, UserSigninUseCase>();



            return services;
        }
    }
}
