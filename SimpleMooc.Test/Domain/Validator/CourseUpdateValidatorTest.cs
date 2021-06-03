using System;
using System.Linq;
using SimpleMooc.Domain.Context.Courses.Command.Input;
using SimpleMooc.Domain.Core.Validations;
using Xunit;

namespace SimpleMooc.Test.Domain.Validator
{
    public class CourseUpdateValidatorTest
    {
        [Fact]
        public async void CourseUpdateCommand_TesteComTodosParametros_success()
        {
            var validator = new CourseValidatorUpdateCommand();
            var command = new CourseUpdateCommand(Guid.NewGuid(), "Curso Teste com XUnit", "Testes com XUnit", null);
            var validationResult = await validator.ValidateAsync(command);

            Assert.True(validationResult.IsValid);
            Assert.False(validationResult.Errors.Any());
        }

        [Theory]
        [InlineData("Teste sem descricao", "")]
        [InlineData("", "Teste sem nome")]
        [InlineData("", "")]
        public async void CourseUpdateCommand_TesteFaltandoEMenoresValoresParametros_Error(string name, string description)
        {
            var validator = new CourseValidatorUpdateCommand();
            var command = new CourseUpdateCommand(Guid.Empty, name, description, null);
            var validationResult = await validator.ValidateAsync(command);

            Assert.False(validationResult.IsValid);
            Assert.True(validationResult.Errors.Any());
        }
    }
}