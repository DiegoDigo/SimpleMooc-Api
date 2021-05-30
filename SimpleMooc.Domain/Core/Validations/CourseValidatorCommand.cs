using FluentValidation;
using SimpleMooc.Domain.Context.Courses.Command.Input;

namespace SimpleMooc.Domain.Core.Validations
{
    public class CourseValidatorCommand : AbstractValidator<CourseCommand>
    {
        public CourseValidatorCommand()
        {
            RuleFor(x => x.Description)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.Image)
                .NotNull();
        }
    }
}