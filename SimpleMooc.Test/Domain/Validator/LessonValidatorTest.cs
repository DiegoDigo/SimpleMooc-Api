using System;
using System.Linq;
using SimpleMooc.Domain.Context.Courses.Command.Input;
using SimpleMooc.Domain.Core.Validations;
using Xunit;

namespace SimpleMooc.Test.Domain.Validator
{
    public class LessonValidatorTest
    {
        [Fact]
        public async void LessonCommand_TesteTodosOsParametros_Success()
        {
            var validator = new LessonValidator();
            var command = new LessonCommand("Teste unitario", "Teste Unitario com xunit", Guid.NewGuid(), null);
            var validationResult = await validator.ValidateAsync(command);

            Assert.True(validationResult.IsValid);
            Assert.False(validationResult.Errors.Any());
        }

        [Theory]
        [InlineData("teste 1", "")]
        [InlineData("", "teste sem nome")]
        [InlineData(null, "teste com nome null")]
        [InlineData("", "")]
        public async void LessonCommand_TesteTodosSemParametros_Error(string name, string description)
        {
            var validator = new LessonValidator();
            var command = new LessonCommand(name, description, Guid.NewGuid(), null);
            var validationResult = await validator.ValidateAsync(command);

            Assert.False(validationResult.IsValid);
            Assert.True(validationResult.Errors.Any());
        }
    }
}