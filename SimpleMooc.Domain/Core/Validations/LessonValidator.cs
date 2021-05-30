using FluentValidation;
using SimpleMooc.Domain.Context.Courses.Command.Input;

namespace SimpleMooc.Domain.Core.Validations
{
    public class LessonValidator : AbstractValidator<LessonCommand>
    {
        public LessonValidator()
        {
            RuleFor(x => x.Description)
                .NotEmpty()
                .NotNull();
            
            RuleFor(x => x.Material)
                .NotEmpty()
                .NotNull();
            
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull();
            
            RuleFor(x => x.CourseId)
                .NotEmpty()
                .NotNull();
        }
    }
}