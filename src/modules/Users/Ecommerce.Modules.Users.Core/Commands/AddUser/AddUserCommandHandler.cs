using Ecommerce.Commons.Abstraction.Helpers;
using Mediator;

namespace Ecommerce.Modules.Users.Core.Commands.AddUser;

public class AddUserCommandHandler : ICommandHandler<AddUserCommand, Result>
{
    public ValueTask<Result> Handle(AddUserCommand command, CancellationToken cancellationToken)
    {
        return new ValueTask<Result>(Result.Success());
    }
}