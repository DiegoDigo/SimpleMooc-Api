using System.Linq;
using Microsoft.AspNetCore.Http;
using Moq;
using SimpleMooc.Domain.Context.Courses.Command.Input;
using SimpleMooc.Domain.Core.Validations;
using Xunit;

namespace SimpleMooc.Test.Domain.Validator
{
    public class CourseValidatorTest
    {
        [Fact]
        public async void CourseCommand_TesteComTodosParametros_success()
        {
            var fileMock = new Mock<IFormFile>();
            var validator = new CourseValidatorCommand();
            var command = new CourseCommand("Curso Teste com XUnit", "Testes com XUnit", fileMock.Object);
            var validationResult = await validator.ValidateAsync(command);

            Assert.True(validationResult.IsValid);
            Assert.False(validationResult.Errors.Any());
        }

        [Theory]
        [InlineData("Teste sem descricao", "")]
        [InlineData("", "Teste sem nome")]
        [InlineData("", "")]
        public async void CourseCommand_TesteFaltandoEMenoresValoresParametros_Error(string name, string description)
        {
            var validator = new CourseValidatorCommand();
            var command = new CourseCommand( name, description, null);
            var validationResult = await validator.ValidateAsync(command);

            Assert.False(validationResult.IsValid);
            Assert.True(validationResult.Errors.Any());
        }
    }
}