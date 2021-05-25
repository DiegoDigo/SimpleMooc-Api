using FluentValidation;
using SimpleMooc.Domain.Context.Users.Command.Input;

namespace SimpleMooc.Domain.Core.Validations
{
    public class ProfileValidator : AbstractValidator<ProfileCommand>
    {
        public ProfileValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .NotNull();
     
            RuleFor(x => x.LastName)
                .NotEmpty()
                .NotNull();

        }
    }
}