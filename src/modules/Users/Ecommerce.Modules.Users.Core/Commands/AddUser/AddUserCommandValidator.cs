using FluentValidation;

namespace Ecommerce.Modules.Users.Core.Commands.AddUser;

public class AddUserCommandValidator : AbstractValidator<AddUserCommand>
{
    public AddUserCommandValidator()
    {
        RuleFor(x => x.RegisterUser.Email).NotEmpty().EmailAddress().WithMessage("Email is required");
        RuleFor(x => x.RegisterUser.Password).NotEmpty().WithMessage("Password is required");
        RuleFor(x => x.RegisterUser.ConfirmPassword)
            .NotEmpty()
            .WithMessage("Confirm Password is required")
            .Equal(x => x.RegisterUser.Password)
            .WithMessage("Passwords do not match");
    }
    
}