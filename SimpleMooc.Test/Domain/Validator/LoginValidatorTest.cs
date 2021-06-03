using System.Linq;
using SimpleMooc.Domain.Context.Users.Command.Input;
using SimpleMooc.Domain.Core.Validations;
using Xunit;

namespace SimpleMooc.Test.Domain.Validator
{
    public class LoginValidatorTest
    {
        [Fact]
        public async void LoginCommand_TesteTodosOsParametros_Success()
        {
            var validator = new LoginValidator();
            var command = new LoginCommand("teste@gmail.com", "12345678");
            var validationResult = await validator.ValidateAsync(command);

            Assert.True(validationResult.IsValid);
            Assert.False(validationResult.Errors.Any());
        }
        
        [Theory]
        [InlineData("teste@gmail.com","")]
        [InlineData("","12345678")]
        [InlineData("teste@gmail.com","1234567")]
        [InlineData("teste.com","12345567")]
        public async void LoginCommand_TesteFaltandoEMenoresValoresParametros_Error(string email, string password)
        {
            var validator = new LoginValidator();
            var command = new LoginCommand(email, password);
            var validationResult = await validator.ValidateAsync(command);

            Assert.False(validationResult.IsValid);
            Assert.True(validationResult.Errors.Any());
        }

    }
}