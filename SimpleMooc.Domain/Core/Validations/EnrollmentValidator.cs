using FluentValidation;
using SimpleMooc.Domain.Context.Courses.Command.Input;

namespace SimpleMooc.Domain.Core.Validations
{
    public class EnrollmentValidator : AbstractValidator<EnrollmentCommand>
    {
        public EnrollmentValidator()
        {
            RuleFor(x => x.CourseId)
                .NotNull()
                .NotEmpty();
        }
    }
}