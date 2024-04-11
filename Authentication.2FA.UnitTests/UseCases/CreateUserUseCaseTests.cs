using Authentication._2FA.Application.DTOs.Request;
using Authentication._2FA.Application.DTOs.Response;
using Authentication._2FA.Application.Interfaces.UseCases;
using Authentication._2FA.Application.UseCases;
using Authentication._2FA.Application.Validations;
using Authentication._2FA.Domain.Entities;
using Authentication._2FA.Domain.Interfaces;
using Authentication._2FA.Shared.Enums;
using Authentication._2FA.Shared.Models;

namespace Authentication._2FA.UnitTests.UseCases
{
    public class CreateUserUseCaseTests
    {
        private readonly IValidator<CreateUserRequestDTO> _CreateUserValidator;
        private readonly Mock<IUserRepository> _UserRepositoryMock;

        public CreateUserUseCaseTests()
        {
            _CreateUserValidator = new CreateUserValidations();
            _UserRepositoryMock = new Mock<IUserRepository>();
        }

        [Fact]
        public async Task Given_CreateUserRequestDTO_When_IsInvalidCredentials_Then_ShouldReturnErrors()
        {
            //Arrange
            CreateUserRequestDTO createUserRequest = new CreateUserRequestDTO()
            {
                Name = "Teste",
                Email = "teste",
                Password = "teste",
            };

            ICreateUserUseCase _CreateUserUseCase = new CreateUserUseCase(_CreateUserValidator, _UserRepositoryMock.Object);

            //Act
            var response = await _CreateUserUseCase.Execute(createUserRequest);

            //Assert
            response.ErrorMessage.Should().NotBeNull();
            response.Status.Should().NotBe(UseCaseResponseKind.Success);
            _UserRepositoryMock.Verify(r => r.Create(It.IsAny<User>()), Times.Never);
        }

        [Fact]
        public async Task Given_CreateUserRequestDTO_When_IsValidCredentials_Then_ShouldReturnSuccess()
        {
            //Arrange
            CreateUserRequestDTO createUserRequest = new CreateUserRequestDTO()
            {
                Name = "Teste",
                Email = "teste@teste.com",
                Password = "teste",
            };

            ICreateUserUseCase _CreateUserUseCase = new CreateUserUseCase(_CreateUserValidator, _UserRepositoryMock.Object);

            //Act
            var response = await _CreateUserUseCase.Execute(createUserRequest);

            //Assert
            response.Errors.Should().BeNull();
            response.Should().BeOfType<UseCaseResponse<MessageSuccessDTO>>();
            response.Status.Should().Be(UseCaseResponseKind.Success);
        }


    }
}
