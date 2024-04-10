using Authentication._2FA.Application.DTOs.Request;
using Authentication._2FA.Shared.Constants;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication._2FA.Application.Validations
{
    public class CreateUserValidations: AbstractValidator<CreateUserRequestDTO>
    {
        public CreateUserValidations()
        {
            RuleFor(u => u.Email).NotEmpty().MaximumLength(50).EmailAddress().WithErrorCode(ErrorCodes.ValidationError);
            RuleFor(u => u.Name).NotEmpty().MinimumLength(3).MaximumLength(100).WithErrorCode(ErrorCodes.ValidationError);
            RuleFor(u => u.Password).NotEmpty().MinimumLength(4).MaximumLength(20).WithErrorCode(ErrorCodes.ValidationError);

        }
    }
}
