using System.Linq;
using SimpleMooc.Domain.Context.Users.Command.Input;
using SimpleMooc.Domain.Core.Validations;
using Xunit;

namespace SimpleMooc.Test.Domain.Validator
{
    public class RefreshTokenValidatorTest
    {
        [Fact]
        public async void RefreshTokenCommand_TesteTodosOsParametros_Success()
        {
            var validator = new RefreshTokenValidator();
            var command = new RefreshTokenCommand("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c");
            var validationResult = await validator.ValidateAsync(command);

            Assert.True(validationResult.IsValid);
            Assert.False(validationResult.Errors.Any());
        }
        
        [Fact]
        public async void RefreshTokenCommand_TesteFaltandoEMenoresValoresParametros_Error()
        {
            var validator = new RefreshTokenValidator();
            var command = new RefreshTokenCommand("");
            var validationResult = await validator.ValidateAsync(command);

            Assert.False(validationResult.IsValid);
            Assert.True(validationResult.Errors.Any());
        }
    }
}