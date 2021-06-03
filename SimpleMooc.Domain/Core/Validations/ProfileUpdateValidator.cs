using FluentValidation;
using SimpleMooc.Domain.Context.Users.Command.Input;

namespace SimpleMooc.Domain.Core.Validations
{
    public class ProfileUpdateValidator : AbstractValidator<ProfileUpdateCommand>
    {
        public ProfileUpdateValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .NotNull();
            
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .NotNull();
            
            RuleFor(x => x.LastName)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.Email)
                .EmailAddress()
                .NotEmpty()
                .NotNull();

        }
    }
}