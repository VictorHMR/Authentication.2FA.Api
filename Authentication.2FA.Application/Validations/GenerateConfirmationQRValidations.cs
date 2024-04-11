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
    public class GenerateConfirmationQRValidations: AbstractValidator<GenerateConfirmationQRRequestDTO>
    {
        public GenerateConfirmationQRValidations()
        {
            RuleFor(u => u.Email).NotEmpty().MaximumLength(50).EmailAddress().WithErrorCode(ErrorCodes.ValidationError);
        }
    }
}
