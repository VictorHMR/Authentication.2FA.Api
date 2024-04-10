using Authentication._2FA.Application.DTOs.Request;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication._2FA.Application.Validations
{
    public class UserSigninValidations: AbstractValidator<UserSigninRequestDTO>
    {
        public UserSigninValidations() {
            RuleFor(u => u.Email).NotEmpty().MaximumLength(50).EmailAddress();
        }
    }
}
