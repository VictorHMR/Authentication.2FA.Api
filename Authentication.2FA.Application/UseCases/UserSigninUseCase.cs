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
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static QRCoder.PayloadGenerator;

namespace Authentication._2FA.Application.UseCases
{
    public class UserSigninUseCase : IUserSigninUseCase
    {
        private readonly IValidator<UserSigninRequestDTO> _Validator;
        private readonly TwoFactorAuthenticator _Tfa;
        private readonly IConfiguration _Config;
        private readonly IUserRepository _UserRepository;


        public UserSigninUseCase(IConfiguration config, IValidator<UserSigninRequestDTO> validator, IUserRepository userRepository)
        {
            _Validator = validator;
            _Tfa = new TwoFactorAuthenticator();
            _Config = config;
            _UserRepository = userRepository;

        }

        public async Task<UseCaseResponse<BearerTokenResponseDTO>> Execute(UserSigninRequestDTO request)
        {
            var result = new UseCaseResponse<BearerTokenResponseDTO>();

            try
            {
                _Validator.ValidateAndThrow(request);
                var user = await _UserRepository.GetUserByEmailPassword(request.Email, HashMD5.HasPassword(request.Password));
                if(user is null)
                    return result.SetInternalServerError(ErrorMessages.WrongUserCredentials, ErrorCodes.WrongUserCredentials);
                else if (request.Google2FA is not null)
                {
                    string key = _Config["Google:2FA_Key"].ToString();
                    if (!_Tfa.ValidateTwoFactorPIN(key, request.Google2FA))
                        return result.SetInternalServerError(ErrorMessages.WrongUserToken, ErrorCodes.WrongUserToken);

                    await _UserRepository.SetLastValidation(user.Id);
                }
                else if (user.LastValidation is null || (DateTime.Now - user.LastValidation) >= TimeSpan.FromDays(30))
                    return result.SetInternalServerError(ErrorMessages.UserValidationError, ErrorCodes.UserValidationError);

                
                return result.SetSuccess(BuildToken(user));
            }
            catch (Exception ex)
            {
                return result.SetInternalServerError(ex.Message);
            }
        }

        public BearerTokenResponseDTO BuildToken(User user)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(JwtRegisteredClaimNames.UniqueName, user.Id.ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

            var Jwtkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_Config["JWT:key"]));

            var creds = new SigningCredentials(Jwtkey, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddHours(23);

            JwtSecurityToken token = new JwtSecurityToken(
               issuer: null,
               audience: null,
               claims: claims,
               expires: expiration,
               signingCredentials: creds);

            return new BearerTokenResponseDTO()
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }

    }
}