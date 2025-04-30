using Ecommerce.Commons.Abstraction.Helpers;
using Mediator;

namespace Ecommerce.Modules.Users.Core.Commands.Login;

public record LoginCommand(LoginDto LoginDto) : ICommand<Result<LoginResult>>;


public class LoginDto
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}

public class LoginResult
{
    public required string Token { get; set; }
}