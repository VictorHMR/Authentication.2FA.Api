using Authentication._2FA.Application.DTOs.Request;
using Authentication._2FA.Application.Validations;

namespace Authentication._2FA.UnitTests.Validations
{
    public class GenerateConfirmationQRValidationsTests
    {
        [Fact]
        public void Given_GenerateConfirmationQRRequest_When_IsValidCredentials_Then_ShouldReturnTrue()
        {
            //Arrange
            IValidator<GenerateConfirmationQRRequestDTO> _GenerateConfirmationQRValidations = new GenerateConfirmationQRValidations();
            GenerateConfirmationQRRequestDTO generateConfirmationQRRequest = new GenerateConfirmationQRRequestDTO()
            {
                Email= "teste@teste.com",
                Password = "teste"
            };

            //Act
            var test = _GenerateConfirmationQRValidations.Validate(generateConfirmationQRRequest);

            //Assert

            test.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Given_GenerateConfirmationQRRequest_When_IsNotValidEmail_Then_ShouldReturnFalse()
        {
            //Arange
            IValidator<GenerateConfirmationQRRequestDTO> _GenerateConfirmationQRValidations = new GenerateConfirmationQRValidations();
            GenerateConfirmationQRRequestDTO generateConfirmationQRRequest = new GenerateConfirmationQRRequestDTO()
            {
                Email = "teste",
                Password = "teste"
            };

            //Act
            var test = _GenerateConfirmationQRValidations.Validate(generateConfirmationQRRequest);

            //Assert
            test.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Given_GenerateConfirmationQRRequest_When_IsToLongEmail_Then_ShouldReturnFalse()
        {
            //Arange
            IValidator<GenerateConfirmationQRRequestDTO> _GenerateConfirmationQRValidations = new GenerateConfirmationQRValidations();
            GenerateConfirmationQRRequestDTO generateConfirmationQRRequest = new GenerateConfirmationQRRequestDTO()
            {
                Email = "testeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee@teste.com",
                Password = "teste"
            };

            //Act
            var test = _GenerateConfirmationQRValidations.Validate(generateConfirmationQRRequest);

            //Assert
            test.IsValid.Should().BeFalse();
        }
    }
}
