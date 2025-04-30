using Ecommerce.Commons.Abstraction.Helpers;
using Ecommerce.Modules.Users.Core.Models;
using FluentValidation;
using Mediator;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Modules.Users.Core.Commands.AddUser;

public class AddUserCommandHandler : ICommandHandler<AddUserCommand, Result>
{
    private readonly UserManager<User> _userManager;
    private readonly IValidator<AddUserCommand> _validator;

    public AddUserCommandHandler(UserManager<User> userManager, IValidator<AddUserCommand> validator)
    {
        _userManager = userManager;
        _validator = validator;
    }

    public async ValueTask<Result> Handle(AddUserCommand command, CancellationToken cancellationToken)
    {
        var validation = await _validator.ValidateAsync(command, cancellationToken);
        if (!validation.IsValid)
        {
            return Result.Failure(ErrorType.ValidationError, string.Join("\n", validation.Errors.Select(e => e.ErrorMessage)));
        }
        
        var existingUser = await _userManager.FindByEmailAsync(command.RegisterUser.Email);
        if (existingUser is not null)
        {
            return Result.Failure(ErrorType.Exists, "User already exists");
        }

        var user = new User()
        {
            Id = Guid.CreateVersion7(),
            Email = command.RegisterUser.Email,
            UserName = command.RegisterUser.Email
        };
        
        var result = await _userManager.CreateAsync(user, command.RegisterUser.Password);
        if (!result.Succeeded)
        {
            return Result.Failure(ErrorType.Exception, string.Join("\n", result.Errors.Select(e => e.Description)));
        }

        await _userManager.AddToRoleAsync(user, nameof(Roles.User));
        
        return Result.Success();
    }
}