using Ecommerce.Commons.Abstraction.Helpers;
using Ecommerce.Modules.Users.Core.Models;
using Ecommerce.Modules.Users.Core.Services;
using FluentValidation;
using Mediator;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Modules.Users.Core.Commands.Login;

public class LoginCommandHandler : ICommandHandler<LoginCommand, Result<LoginResult>>
{
    private readonly UserManager<User> _userManager;
    private readonly IValidator<LoginCommand> _validator;
    private readonly ITokenService _tokenService;

    public LoginCommandHandler(UserManager<User> userManager, IValidator<LoginCommand> validator, ITokenService tokenService)
    {
        _userManager = userManager;
        _validator = validator;
        _tokenService = tokenService;
    }

    public async ValueTask<Result<LoginResult>> Handle(LoginCommand command, CancellationToken cancellationToken)
    {
        var validator = await _validator.ValidateAsync(command, cancellationToken);
        if (!validator.IsValid)
        {
            return Result<LoginResult>.Failure(ErrorType.ValidationError, string.Join("\n", validator.Errors.Select(e => e.ErrorMessage)));
        }
        var user = await _userManager.FindByEmailAsync(command.LoginDto.Email);
        if (user is null)
        {
            return Result<LoginResult>.Failure(ErrorType.NotFound,"Invalid email or password");
        }

        var isPasswordValid = await _userManager.CheckPasswordAsync(user, command.LoginDto.Password);
        if (!isPasswordValid)
        {
            return Result<LoginResult>.Failure(ErrorType.NotFound,"Invalid email or password");
        }

        var token = await _tokenService.GenerateJwtTokenAsync(user);
        
        return Result<LoginResult>.Success(new LoginResult{Token = token});
    }
}