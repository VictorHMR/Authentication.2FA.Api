using Authentication._2FA.Application.DTOs.Request;
using Authentication._2FA.Application.Validations;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Authentication._2FA.IoC.Validator
{
    public static class ValidatorInjections
    {
        public static IServiceCollection AddValidator(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IValidator<CreateUserRequestDTO>, CreateUserValidations>();


            return services;
        }
    }
}
