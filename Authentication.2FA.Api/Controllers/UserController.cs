using Authentication._2FA.Application.DTOs.Request;
using Authentication._2FA.Application.Interfaces.UseCases;
using Authentication._2FA.Shared.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using static QRCoder.PayloadGenerator;

namespace Authentication._2FA.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IActionResultConverter _ActionResultConverter;
        private readonly ICreateUserUseCase _CreateUserUseCase;
        private readonly IGenerateConfirmationQRUseCase _GenerateConfirmationQR;
        private readonly IUserSigninUseCase _UserSigninUseCase;


        public UserController(
             IActionResultConverter actionResultConverter,
             ICreateUserUseCase createUserUseCase,
             IGenerateConfirmationQRUseCase generateConfirmationQR,
             IUserSigninUseCase userSigninUseCase
            ) {
            _ActionResultConverter = actionResultConverter;
            _CreateUserUseCase = createUserUseCase;
            _GenerateConfirmationQR = generateConfirmationQR;
            _UserSigninUseCase = userSigninUseCase;

        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser([FromForm] CreateUserRequestDTO request)
        {
            try
            {
                var response = await _CreateUserUseCase.Execute(request);
                return _ActionResultConverter.Convert(response);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex);
            }
        }

        [HttpPost("GenerateConfirmationQR")]
        public async Task<IActionResult> GenerateConfirmationQR([FromForm] GenerateConfirmationQRRequestDTO request)
        {
            try
            {
                var response = await _GenerateConfirmationQR.Execute(request);
                return _ActionResultConverter.Convert(response);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex);
            }
        }

        [HttpPost("UserSignin")]
        public async Task<IActionResult> UserSignin([FromForm] UserSigninRequestDTO request)
        {
            try
            {
                var response = await _UserSigninUseCase.Execute(request);
                return _ActionResultConverter.Convert(response);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex);
            }
        }

    }
}
