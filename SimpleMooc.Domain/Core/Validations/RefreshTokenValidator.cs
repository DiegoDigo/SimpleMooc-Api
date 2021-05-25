using FluentValidation;
using SimpleMooc.Domain.Context.Users.Command.Input;

namespace SimpleMooc.Domain.Core.Validations
{
    public class RefreshTokenValidator : AbstractValidator<RefreshTokenCommand>
    {
        public RefreshTokenValidator()
        {
            RuleFor(x => x.Refresh)
                .NotEmpty()
                .NotNull()
                .MinimumLength(64);
        }
    }
}