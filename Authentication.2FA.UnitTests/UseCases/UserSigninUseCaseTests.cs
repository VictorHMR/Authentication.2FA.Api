using Authentication._2FA.Application.DTOs.Request;
using Authentication._2FA.Application.DTOs.Response;
using Authentication._2FA.Application.Interfaces.UseCases;
using Authentication._2FA.Application.UseCases;
using Authentication._2FA.Application.Validations;
using Authentication._2FA.Domain.Entities;
using Authentication._2FA.Domain.Interfaces;
using Authentication._2FA.Shared.Enums;
using Authentication._2FA.Shared.Interfaces;
using Authentication._2FA.Shared.Models;
using Google.Authenticator;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication._2FA.UnitTests.UseCases
{
    public class UserSigninUseCaseTests
    {
        private readonly IValidator<UserSigninRequestDTO> _UserSigninRequestValidator;
        private readonly Mock<IConfiguration> _ConfigMock;
        private readonly Mock<IUserRepository> _UserRepositoryMock;
        private readonly Mock<TwoFactorAuthenticator> _TfaMock;


        public UserSigninUseCaseTests()
        {
            _UserSigninRequestValidator = new UserSigninValidations();
            _UserRepositoryMock = new Mock<IUserRepository>();
            _ConfigMock = new Mock<IConfiguration>();
            _TfaMock = new Mock<TwoFactorAuthenticator>();

        }
        [Fact]
        public async void Given_UserSigninRequest_When_Google2FAIsNull_ShouldReturnBearerTokenResponseDTO()
        {
            //Arrange
            UserSigninRequestDTO UserSigninRequest = new UserSigninRequestDTO()
            {
                Email = "teste@teste.com",
                Password = "teste"
            };

            IUserSigninUseCase _UserSigninUseCase = new UserSigninUseCase(_ConfigMock.Object, _UserSigninRequestValidator, _UserRepositoryMock.Object);
            _ConfigMock.SetupGet(x => x["Google:2FA_Key"]).Returns("ffcb23a5-ee1b-44ca-a7a4-39298c9f7a84");
            _ConfigMock.SetupGet(x => x["JWT:key"]).Returns("ubo5N4BsIhzxG8JE3kS5sRyPMOFlwTqL"); //Gerada aleatoriamente para teste
            _UserRepositoryMock.Setup(s => s.GetUserByEmailPassword(UserSigninRequest.Email, HashMD5.HasPassword(UserSigninRequest.Password))).ReturnsAsync(new User(1, "teste", UserSigninRequest.Email, UserSigninRequest.Password, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now));

            //Act
            var response = await _UserSigninUseCase.Execute(UserSigninRequest);


            //Assert
            response.ErrorMessage.Should().BeNull();
            response.Should().BeOfType<UseCaseResponse<BearerTokenResponseDTO>>();
            response.Status.Should().Be(UseCaseResponseKind.Success);
            _UserRepositoryMock.Verify(r => r.SetLastValidation(It.IsAny<int>()), Times.Never);

        }
        [Fact]
        public async void Given_UserSigninRequest_When_InvalidCredentials_ShouldReturnError()
        {
            //Arrange
            UserSigninRequestDTO UserSigninRequest = new UserSigninRequestDTO()
            {
                Email = "teste",
                Password = "teste"
            };

            IUserSigninUseCase _UserSigninUseCase = new UserSigninUseCase(_ConfigMock.Object, _UserSigninRequestValidator, _UserRepositoryMock.Object);
            _ConfigMock.SetupGet(x => x["Google:2FA_Key"]).Returns("ffcb23a5-ee1b-44ca-a7a4-39298c9f7a84");
            _ConfigMock.SetupGet(x => x["JWT:key"]).Returns("ubo5N4BsIhzxG8JE3kS5sRyPMOFlwTqL"); //Gerada aleatoriamente para teste
            _UserRepositoryMock.Setup(s => s.GetUserByEmailPassword(UserSigninRequest.Email, HashMD5.HasPassword(UserSigninRequest.Password))).ReturnsAsync(new User(1, "teste", UserSigninRequest.Email, UserSigninRequest.Password, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now));

            //Act
            var response = await _UserSigninUseCase.Execute(UserSigninRequest);


            //Assert
            response.ErrorMessage.Should().NotBeNull();
            response.Status.Should().NotBe(UseCaseResponseKind.Success);
            _UserRepositoryMock.Verify(r => r.SetLastValidation(It.IsAny<int>()), Times.Never);

        }

        [Fact]
        public async void Given_UserSigninRequest_When_UserNotExists_ShouldReturnError()
        {
            //Arrange
            UserSigninRequestDTO UserSigninRequest = new UserSigninRequestDTO()
            {
                Email = "teste@teste.com",
                Password = "teste"
            };

            IUserSigninUseCase _UserSigninUseCase = new UserSigninUseCase(_ConfigMock.Object, _UserSigninRequestValidator, _UserRepositoryMock.Object);
            _ConfigMock.SetupGet(x => x["Google:2FA_Key"]).Returns("ffcb23a5-ee1b-44ca-a7a4-39298c9f7a84");
            _ConfigMock.SetupGet(x => x["JWT:key"]).Returns("ubo5N4BsIhzxG8JE3kS5sRyPMOFlwTqL"); //Gerada aleatoriamente para teste
            _UserRepositoryMock.Setup(s => s.GetUserByEmailPassword(UserSigninRequest.Email, HashMD5.HasPassword(UserSigninRequest.Password))).ReturnsAsync((User)null);

            //Act
            var response = await _UserSigninUseCase.Execute(UserSigninRequest);


            //Assert
            response.ErrorMessage.Should().NotBeNull();
            response.Status.Should().NotBe(UseCaseResponseKind.Success);
            _UserRepositoryMock.Verify(r => r.SetLastValidation(It.IsAny<int>()), Times.Never);

        }

        [Fact]
        public async void Given_UserSigninRequest_When_UserHasInvalidLastValidation_ShouldReturnErros()
        {
            //Arrange
            UserSigninRequestDTO UserSigninRequest = new UserSigninRequestDTO()
            {
                Email = "teste@teste.com",
                Password = "teste"
            };

            IUserSigninUseCase _UserSigninUseCase = new UserSigninUseCase(_ConfigMock.Object, _UserSigninRequestValidator, _UserRepositoryMock.Object);
            _ConfigMock.SetupGet(x => x["Google:2FA_Key"]).Returns("ffcb23a5-ee1b-44ca-a7a4-39298c9f7a84");
            _ConfigMock.SetupGet(x => x["JWT:key"]).Returns("ubo5N4BsIhzxG8JE3kS5sRyPMOFlwTqL"); //Gerada aleatoriamente para teste
            _UserRepositoryMock.Setup(s => s.GetUserByEmailPassword(UserSigninRequest.Email, HashMD5.HasPassword(UserSigninRequest.Password))).ReturnsAsync(new User(1, "teste", UserSigninRequest.Email, UserSigninRequest.Password, DateTime.Now, DateTime.Now.AddDays(-31), DateTime.Now, DateTime.Now));

            //Act
            var response = await _UserSigninUseCase.Execute(UserSigninRequest);


            //Assert
            response.ErrorMessage.Should().NotBeNull();
            response.Status.Should().NotBe(UseCaseResponseKind.Success);
            _UserRepositoryMock.Verify(r => r.SetLastValidation(It.IsAny<int>()), Times.Never);

        }

        [Fact]
        public async void Given_UserSigninRequest_When_Google2FAIsNotNullButInvalid_ShouldReturnErros()
        {
            //Arrange
            UserSigninRequestDTO UserSigninRequest = new UserSigninRequestDTO()
            {
                Email = "teste@teste.com",
                Password = "teste",
                Google2FA = "123456"
            };

            IUserSigninUseCase _UserSigninUseCase = new UserSigninUseCase(_ConfigMock.Object, _UserSigninRequestValidator, _UserRepositoryMock.Object);
            _ConfigMock.SetupGet(x => x["Google:2FA_Key"]).Returns("ffcb23a5-ee1b-44ca-a7a4-39298c9f7a84");
            _ConfigMock.SetupGet(x => x["JWT:key"]).Returns("ubo5N4BsIhzxG8JE3kS5sRyPMOFlwTqL"); //Gerada aleatoriamente para teste
            _UserRepositoryMock.Setup(s => s.GetUserByEmailPassword(UserSigninRequest.Email, HashMD5.HasPassword(UserSigninRequest.Password))).ReturnsAsync(new User(1, "teste", UserSigninRequest.Email, UserSigninRequest.Password, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now));

            //Act
            var response = await _UserSigninUseCase.Execute(UserSigninRequest);


            //Assert
            response.ErrorMessage.Should().NotBeNull();
            response.Status.Should().NotBe(UseCaseResponseKind.Success);
            _UserRepositoryMock.Verify(r => r.SetLastValidation(It.IsAny<int>()), Times.Never);

        }

        [Fact]
        public async void Given_UserSigninRequest_When_Google2FAIsNotNull_ShouldReturnErros()
        {
            //Arrange
            UserSigninRequestDTO UserSigninRequest = new UserSigninRequestDTO()
            {
                Email = "teste@teste.com",
                Password = "teste",
                Google2FA = "330370"
            };

            IUserSigninUseCase _UserSigninUseCase = new UserSigninUseCase(_ConfigMock.Object, _UserSigninRequestValidator, _UserRepositoryMock.Object);
            _ConfigMock.SetupGet(x => x["Google:2FA_Key"]).Returns("ffcb23a5-ee1b-44ca-a7a4-39298c9f7a84");
            _ConfigMock.SetupGet(x => x["JWT:key"]).Returns("ubo5N4BsIhzxG8JE3kS5sRyPMOFlwTqL"); //Gerada aleatoriamente para teste
            _UserRepositoryMock.Setup(s => s.GetUserByEmailPassword(UserSigninRequest.Email, HashMD5.HasPassword(UserSigninRequest.Password))).ReturnsAsync(new User(1, "teste", UserSigninRequest.Email, UserSigninRequest.Password, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now));
            //Futuramente será preciso mockar o método ValidateTwoFactorPIN, atualmente não encontrei uma forma, pois o mesmo não possui interface.

            //Act
            var response = await _UserSigninUseCase.Execute(UserSigninRequest);


            //Assert
            response.ErrorMessage.Should().BeNull();
            response.Should().BeOfType<UseCaseResponse<BearerTokenResponseDTO>>();
            response.Status.Should().Be(UseCaseResponseKind.Success);

        }

    }
}
