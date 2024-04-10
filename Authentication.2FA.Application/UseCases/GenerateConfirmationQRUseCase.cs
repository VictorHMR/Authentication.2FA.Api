

using Authentication._2FA.Application.DTOs.Request;
using Authentication._2FA.Application.DTOs.Response;
using Authentication._2FA.Application.Interfaces.UseCases;
using Authentication._2FA.Domain.Entities;
using Authentication._2FA.Domain.Interfaces;
using Authentication._2FA.Shared.Constants;
using Authentication._2FA.Shared.Models;
using FluentValidation;
using Google.Authenticator;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static QRCoder.PayloadGenerator;

namespace Authentication._2FA.Application.UseCases
{
    public class GenerateConfirmationQRUseCase : IGenerateConfirmationQRUseCase
    {
        private readonly IValidator<GenerateConfirmationQRRequestDTO> _Validator;
        private readonly TwoFactorAuthenticator _Tfa;
        private readonly IConfiguration _Config;
        private readonly IUserRepository _UserRepository;


        public GenerateConfirmationQRUseCase(IConfiguration config, IValidator<GenerateConfirmationQRRequestDTO> validator, IUserRepository userRepository)
        {
            _Validator = validator;
            _Tfa = new TwoFactorAuthenticator();
            _Config = config;
            _UserRepository = userRepository;

        }

        public async Task<UseCaseResponse<GenerateConfirmationQRResponseDTO>> Execute(GenerateConfirmationQRRequestDTO request)
        {
            var result = new UseCaseResponse<GenerateConfirmationQRResponseDTO>();

            try
            {
                _Validator.ValidateAndThrow(request);
                string key = _Config["Google:2FA_Key"].ToString();

                var user = await _UserRepository.GetUserByEmailPassword(request.Email, HashMD5.HasPassword(request.Password));
                if (user is null)
                    return result.SetInternalServerError(ErrorMessages.WrongUserCredentials, ErrorCodes.WrongUserCredentials);

                SetupCode setupInfo = _Tfa.GenerateSetupCode("Authentication.2FA", user.Email, key, false, 3);


                return result.SetSuccess(new GenerateConfirmationQRResponseDTO()
                {
                    QRCodeBase64 = setupInfo.QrCodeSetupImageUrl
                });

            }
            catch (Exception ex)
            {
                return result.SetInternalServerError(ex.Message);
            }
        }

    }
}
