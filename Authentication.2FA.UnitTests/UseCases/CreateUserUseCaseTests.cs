using Authentication._2FA.Application.DTOs.Request;
using Authentication._2FA.Application.Interfaces.UseCases;
using Authentication._2FA.Application.UseCases;
using Authentication._2FA.Application.Validations;
using Authentication._2FA.Domain.Entities;
using Authentication._2FA.Domain.Interfaces;

namespace Authentication._2FA.UnitTests.CreateUser
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
        public async Task Given_CreateUserRequestDTO_When_IsInvalidCredentials_Then_ShouldReturnFalse()
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
            response.Success().Should().BeFalse();
            _UserRepositoryMock.Verify(r => r.Create(It.IsAny<User>()), Times.Never);
        }

        [Fact]
        public async Task Given_CreateUserRequestDTO_When_IsValidCredentials_Then_ShouldReturnTrue()
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
            response.Success().Should().BeTrue();
        }
    }
}
