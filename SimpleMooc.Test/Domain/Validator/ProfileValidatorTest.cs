using System.Linq;
using SimpleMooc.Domain.Context.Users.Command.Input;
using SimpleMooc.Domain.Core.Validations;
using Xunit;

namespace SimpleMooc.Test.Domain.Validator
{
    public class ProfileValidatorTest
    {
        [Fact]
        public async void ProfileCommand_TesteTodosOsParametros_Success()
        {
            var validator = new ProfileValidator();
            var command = new ProfileCommand("Diego", "Domingos Delmiro", null);
            var validationResult = await validator.ValidateAsync(command);

            Assert.True(validationResult.IsValid);
            Assert.False(validationResult.Errors.Any());
        }
        
        [Theory]
        [InlineData("Diego","")]
        [InlineData("","Delmiro")]
        [InlineData("","")]
        public async void ProfileCommand_TesteFaltandoEMenoresValoresParametros_Error(string firstName, string lastName)
        {
            var validator = new ProfileValidator();
            var command = new ProfileCommand(firstName, lastName, null);
            var validationResult = await validator.ValidateAsync(command);

            Assert.False(validationResult.IsValid);
            Assert.True(validationResult.Errors.Any());
        }

    }
}