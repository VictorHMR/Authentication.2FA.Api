using Authentication._2FA.Application.DTOs.Request;
using Authentication._2FA.Application.Validations;

namespace Authentication._2FA.UnitTests.Validations
{
    public class UserSigninValidationsTests
    {
        [Fact]
        public void Given_UserSigninRequest_When_IsValidCredentialsButWithoutGoogle2FA_Then_ShouldReturnTrue()
        {
            //Arrange
            IValidator<UserSigninRequestDTO> _UserSigninValidations = new UserSigninValidations();
            UserSigninRequestDTO userSigninRequest = new UserSigninRequestDTO()
            {
                Email = "teste@teste.com",
                Password = "teste"
            };

            //Act
            var test = _UserSigninValidations.Validate(userSigninRequest);

            //Assert

            test.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Given_UserSigninRequest_When_IsValidCredentialsThen_ShouldReturnTrue()
        {
            //Arrange
            IValidator<UserSigninRequestDTO> _UserSigninValidations = new UserSigninValidations();
            UserSigninRequestDTO userSigninRequest = new UserSigninRequestDTO()
            {
                Email = "teste@teste.com",
                Password = "teste",
                Google2FA = "123456"
            };

            //Act
            var test = _UserSigninValidations.Validate(userSigninRequest);

            //Assert

            test.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Given_UserSigninRequest_When_IsNotValidEmail_Then_ShouldReturnFalse()
        {
            //Arange
            IValidator<UserSigninRequestDTO> _UserSigninValidations = new UserSigninValidations();
            UserSigninRequestDTO userSigninRequest = new UserSigninRequestDTO()
            {
                Email = "teste",
                Password = "teste"
            };

            //Act
            var test = _UserSigninValidations.Validate(userSigninRequest);

            //Assert
            test.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Given_UserSigninRequest_When_IsToLongEmail_Then_ShouldReturnFalse()
        {
            //Arange
            IValidator<UserSigninRequestDTO> _UserSigninValidations = new UserSigninValidations();
            UserSigninRequestDTO userSigninRequest = new UserSigninRequestDTO()
            {
                Email = "testeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee@teste.com",
                Password = "teste"
            };

            //Act
            var test = _UserSigninValidations.Validate(userSigninRequest);

            //Assert
            test.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Given_UserSigninRequest_When_IsEmptyGoogle2FA_Then_ShouldReturnFalse()
        {
            //Arange
            IValidator<UserSigninRequestDTO> _UserSigninValidations = new UserSigninValidations();
            UserSigninRequestDTO userSigninRequest = new UserSigninRequestDTO()
            {
                Email = "teste@teste.com",
                Password = "teste",
                Google2FA = ""
            };

            //Act
            var test = _UserSigninValidations.Validate(userSigninRequest);

            //Assert
            test.IsValid.Should().BeFalse();
        }
    }
}
