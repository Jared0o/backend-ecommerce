using Ecommerce.Commons.Abstraction.Helpers;
using Mediator;

namespace Ecommerce.Modules.Users.Core.Commands.AddUser;

public record AddUserCommand(RegisterUserDto RegisterUser) : ICommand<Result>;

public record RegisterUserDto(string Email, string Password, string ConfirmPassword) : ICommand;