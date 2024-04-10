using Authentication._2FA.Application.DTOs.Request;
using Authentication._2FA.Application.DTOs.Response;
using Authentication._2FA.Application.Interfaces.UseCases;
using Authentication._2FA.Domain.Entities;
using Authentication._2FA.Domain.Interfaces;
using Authentication._2FA.Shared.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication._2FA.Application.UseCases
{
    public class CreateUserUseCase: ICreateUserUseCase
    {
        private readonly IValidator<CreateUserRequestDTO> _CreateUserValidator;
        private readonly IUserRepository _UserRepository;

        public CreateUserUseCase(
            IValidator<CreateUserRequestDTO> createUserValidator,
            IUserRepository userRepository) 
        {
            _CreateUserValidator = createUserValidator;
            _UserRepository = userRepository;
        }

        public async Task<UseCaseResponse<MessageSuccessDTO>> Execute(CreateUserRequestDTO request)
        {
            var result = new UseCaseResponse<MessageSuccessDTO>();

            try
            {
                _CreateUserValidator.ValidateAndThrow(request);
                await _UserRepository.Create(new User(request.Name, request.Email, request.Password));
                return result.SetSuccess(new MessageSuccessDTO("Usuário criado com sucesso!"));

            }
            catch (Exception ex)
            {
                return result.SetInternalServerError(ex.Message);
            }
        }

    }
}
