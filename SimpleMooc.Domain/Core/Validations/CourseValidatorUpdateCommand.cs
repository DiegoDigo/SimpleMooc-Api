using FluentValidation;
using SimpleMooc.Domain.Context.Courses.Command.Input;

namespace SimpleMooc.Domain.Core.Validations
{
    public class CourseValidatorUpdateCommand : AbstractValidator<CourseUpdateCommand>
    {
        public CourseValidatorUpdateCommand()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.Description)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull();
        }
    }
}