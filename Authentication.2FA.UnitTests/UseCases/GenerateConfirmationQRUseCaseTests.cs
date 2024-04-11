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
using Microsoft.Extensions.Configuration;

namespace Authentication._2FA.UnitTests.UseCases
{
    public class GenerateConfirmationQRUseCaseTests
    {
        private readonly IValidator<GenerateConfirmationQRRequestDTO> _GenerateConfirmationQRValidator;
        private readonly Mock<IConfiguration> _ConfigMock;
        private readonly Mock<IUserRepository> _UserRepositoryMock;

        public GenerateConfirmationQRUseCaseTests()
        {
            _GenerateConfirmationQRValidator = new GenerateConfirmationQRValidations();
            _UserRepositoryMock = new Mock<IUserRepository>();
            _ConfigMock = new Mock<IConfiguration>();
        }

        [Fact]
        public async void Given_GenerateConfirmationQR_When_IsValidCredentials_And_UserExists_ShouldReturnGenerateConfirmationQRResponseDTO()
        {
            //Arrange
            GenerateConfirmationQRRequestDTO GenerateConfirmationQRRequest = new GenerateConfirmationQRRequestDTO()
            {
                Email = "teste@teste.com",
                Password = "teste"
            };

            IGenerateConfirmationQRUseCase _GenerateConfirmationQRUseCase = new GenerateConfirmationQRUseCase(_ConfigMock.Object, _GenerateConfirmationQRValidator,_UserRepositoryMock.Object);
            _ConfigMock.SetupGet(x => x["Google:2FA_Key"]).Returns("keyusada para teste");
            _UserRepositoryMock.Setup(s => s.GetUserByEmailPassword(GenerateConfirmationQRRequest.Email, HashMD5.HasPassword(GenerateConfirmationQRRequest.Password))).ReturnsAsync(new User(1, "teste", GenerateConfirmationQRRequest.Email, GenerateConfirmationQRRequest.Password, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now));

            //Act
            var response = await _GenerateConfirmationQRUseCase.Execute(GenerateConfirmationQRRequest);


            //Assert
            response.ErrorMessage.Should().BeNull();
            response.Should().BeOfType<UseCaseResponse<GenerateConfirmationQRResponseDTO>>();
            response.Status.Should().Be(UseCaseResponseKind.Success);

        }

        [Fact]
        public async void Given_GenerateConfirmationQR_When_IsNotValidCredntials_And_UserNotExists_ShouldReturnErrors()
        {
            //Arrange
            GenerateConfirmationQRRequestDTO GenerateConfirmationQRRequest = new GenerateConfirmationQRRequestDTO()
            {
                Email = "teste",
                Password = "teste"
            };

            IGenerateConfirmationQRUseCase _GenerateConfirmationQRUseCase = new GenerateConfirmationQRUseCase(_ConfigMock.Object, _GenerateConfirmationQRValidator, _UserRepositoryMock.Object);
            _ConfigMock.SetupGet(x => x["Google:2FA_Key"]).Returns("keyusada para teste");
            _UserRepositoryMock.Setup(s => s.GetUserByEmailPassword(GenerateConfirmationQRRequest.Email, HashMD5.HasPassword(GenerateConfirmationQRRequest.Password))).ReturnsAsync((User)null);

            //Act
            var response = await _GenerateConfirmationQRUseCase.Execute(GenerateConfirmationQRRequest);


            //Assert
            response.ErrorMessage.Should().NotBeNull();
            response.Status.Should().NotBe(UseCaseResponseKind.Success);

        }

        [Fact]
        public async void Given_GenerateConfirmationQR_When_IsValidCredentials_And_UserNotExists_ShouldReturnErrors()
        {
            //Arrange
            GenerateConfirmationQRRequestDTO GenerateConfirmationQRRequest = new GenerateConfirmationQRRequestDTO()
            {
                Email = "teste@teste.com",
                Password = "teste"
            };

            IGenerateConfirmationQRUseCase _GenerateConfirmationQRUseCase = new GenerateConfirmationQRUseCase(_ConfigMock.Object, _GenerateConfirmationQRValidator, _UserRepositoryMock.Object);
            _ConfigMock.SetupGet(x => x["Google:2FA_Key"]).Returns("keyusada para teste");
            _UserRepositoryMock.Setup(s => s.GetUserByEmailPassword(GenerateConfirmationQRRequest.Email, HashMD5.HasPassword(GenerateConfirmationQRRequest.Password))).ReturnsAsync((User)null);

            //Act
            var response = await _GenerateConfirmationQRUseCase.Execute(GenerateConfirmationQRRequest);


            //Assert
            response.ErrorMessage.Should().NotBeNull();
            response.Status.Should().NotBe(UseCaseResponseKind.Success);

        }

    }
}
