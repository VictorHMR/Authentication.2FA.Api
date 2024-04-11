using Authentication._2FA.Application.DTOs.Request;
using Authentication._2FA.Application.Validations;
using FluentAssertions;
using FluentValidation;

namespace Authentication._2FA.UnitTests
{
    public class CreateUserValidationsTests
    {
        [Fact]
        public void Given_CreateUserRequest_When_IsValidCredentials_Then_ShouldReturnTrue()
        {
            //Arange
            IValidator<CreateUserRequestDTO> _CreateUserValidator = new CreateUserValidations();

            CreateUserRequestDTO createUserRequest = new CreateUserRequestDTO()
            {
                Name = "Teste",
                Email = "teste@teste.com",
                Password = "teste",
            };

            //Act
            var test = _CreateUserValidator.Validate(createUserRequest);

            //Assert
            test.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Given_CreateUserRequest_When_IsNotValidEmail_Then_ShouldReturnFalse()
        {
            //Arange
            IValidator<CreateUserRequestDTO> _CreateUserValidator = new CreateUserValidations();

            CreateUserRequestDTO createUserRequest = new CreateUserRequestDTO()
            {
                Name = "Teste",
                Email = "teste",
                Password = "teste",
            };

            //Act
            var test = _CreateUserValidator.Validate(createUserRequest);

            //Assert
            test.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Given_CreateUserRequest_When_IsToLongEmail_Then_ShouldReturnFalse()
        {
            //Arange
            IValidator<CreateUserRequestDTO> _CreateUserValidator = new CreateUserValidations();

            CreateUserRequestDTO createUserRequest = new CreateUserRequestDTO()
            {
                Name = "Teste",
                Email = "testeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee@teste.com",
                Password = "teste",
            };

            //Act
            var test = _CreateUserValidator.Validate(createUserRequest);

            //Assert
            test.IsValid.Should().BeFalse();
        }


        [Fact]
        public void Given_CreateUserRequest_When_IsToLongName_Then_ShouldReturnFalse()
        {
            //Arange
            IValidator<CreateUserRequestDTO> _CreateUserValidator = new CreateUserValidations();

            CreateUserRequestDTO createUserRequest = new CreateUserRequestDTO()
            {
                Name = "Testeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee",
                Email = "teste@teste.com",
                Password = "teste",
            };

            //Act
            var test = _CreateUserValidator.Validate(createUserRequest);

            //Assert
            test.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Given_CreateUserRequest_When_IsToShortName_Then_ShouldReturnFalse()
        {
            //Arange
            IValidator<CreateUserRequestDTO> _CreateUserValidator = new CreateUserValidations();

            CreateUserRequestDTO createUserRequest = new CreateUserRequestDTO()
            {
                Name = "te",
                Email = "teste@teste.com",
                Password = "teste",
            };

            //Act
            var test = _CreateUserValidator.Validate(createUserRequest);

            //Assert
            test.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Given_CreateUserRequest_When_IsToShortPassword_Then_ShouldReturnFalse()
        {
            //Arange
            IValidator<CreateUserRequestDTO> _CreateUserValidator = new CreateUserValidations();

            CreateUserRequestDTO createUserRequest = new CreateUserRequestDTO()
            {
                Name = "teste",
                Email = "teste@teste.com",
                Password = "tes",
            };

            //Act
            var test = _CreateUserValidator.Validate(createUserRequest);

            //Assert
            test.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Given_CreateUserRequest_When_IsToLongPassword_Then_ShouldReturnFalse()
        {
            //Arange
            IValidator<CreateUserRequestDTO> _CreateUserValidator = new CreateUserValidations();

            CreateUserRequestDTO createUserRequest = new CreateUserRequestDTO()
            {
                Name = "teste",
                Email = "teste@teste.com",
                Password = "testeeeeeeeeeeeeeeeee",
            };

            //Act
            var test = _CreateUserValidator.Validate(createUserRequest);

            //Assert
            test.IsValid.Should().BeFalse();
        }

    }
}