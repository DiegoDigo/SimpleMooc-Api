using System;
using System.Linq;
using SimpleMooc.Domain.Context.Courses.Command.Input;
using SimpleMooc.Domain.Core.Validations;
using Xunit;

namespace SimpleMooc.Test.Domain.Validator
{
    public class EnrollmentValidatorTest
    {
        [Fact]
        public async void EnrollmentCommand_TesteTodosOsParametros_Success()
        {
            var validator = new EnrollmentValidator();
            var command = new EnrollmentCommand(Guid.NewGuid());
            var validationResult = await validator.ValidateAsync(command);

            Assert.True(validationResult.IsValid);
            Assert.False(validationResult.Errors.Any());
        }

        [Fact]
        public async void EnrollmentCommand_TesteTodosSemParametros_Error()
        {
            var validator = new EnrollmentValidator();
            var command = new EnrollmentCommand(Guid.Empty);
            var validationResult = await validator.ValidateAsync(command);

            Assert.False(validationResult.IsValid);
            Assert.True(validationResult.Errors.Any());
        }
    }
}