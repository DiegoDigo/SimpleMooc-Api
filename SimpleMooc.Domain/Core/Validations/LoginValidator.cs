using FluentValidation;
using SimpleMooc.Domain.Context.Users.Command.Input;

namespace SimpleMooc.Domain.Core.Validations
{
    public class LoginValidator : AbstractValidator<LoginCommand>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Email).EmailAddress();
            
            RuleFor(x => x.Password)
                .NotEmpty()
                .NotNull()
                .Length(8);
        }
    }
}