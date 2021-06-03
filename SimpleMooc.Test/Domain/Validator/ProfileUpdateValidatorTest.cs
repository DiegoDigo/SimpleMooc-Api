using System;
using System.Linq;
using SimpleMooc.Domain.Context.Users.Command.Input;
using SimpleMooc.Domain.Core.Validations;
using Xunit;

namespace SimpleMooc.Test.Domain.Validator
{
    public class ProfileUpdateValidatorTest
    {
        [Fact]
        public async void ProfileUpdateCommand_TesteTodosOsParametros_Success()
        {
            var validator = new ProfileUpdateValidator();
            var command =
                new ProfileUpdateCommand(Guid.NewGuid(), "Diego", "Domingos Delmiro", null, "teste@gmail.com");
            var validationResult = await validator.ValidateAsync(command);

            Assert.True(validationResult.IsValid);
            Assert.False(validationResult.Errors.Any());
        }

        [Theory]
        [InlineData("Diego", "", "teste@gmail.com")]
        [InlineData("", "Delmiro", "testegmail.com")]
        [InlineData("", "", "")]
        public async void ProfileUpdateCommand_TesteFaltandoEMenoresValoresParametros_Error(string firstName, string lastName, string email)
        {
            var validator = new ProfileUpdateValidator();
            var command =
                new ProfileUpdateCommand(Guid.NewGuid(), firstName, lastName, null, email);
            var validationResult = await validator.ValidateAsync(command);

            Assert.False(validationResult.IsValid);
            Assert.True(validationResult.Errors.Any());
        }
    }
}