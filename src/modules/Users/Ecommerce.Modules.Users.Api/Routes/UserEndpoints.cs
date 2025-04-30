using Ecommerce.Commons.Infrastructure.Api;
using Ecommerce.Modules.Users.Core.Commands.AddUser;
using Ecommerce.Modules.Users.Core.Commands.Login;
using Mediator;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Ecommerce.Modules.Users.Api.Routes;

public static class UserEndpoints
{
    public static RouteGroupBuilder MapUserEndpoints(this RouteGroupBuilder group)
    {
        group.MapPost("/register", CreateUser);
        group.MapPost("/login", LoginUser);
        group.MapGet("/", () => "Its Users Module");

        return group;
    }

    private static async Task<IResult> CreateUser([FromBody] RegisterUserDto userDto, [FromServices] ISender mediator)
    {
        var res = await mediator.Send(new AddUserCommand(userDto));

        return res.IsSuccess ? Results.NoContent() : ApiResult.HandleError(res.Error);
    }
    private static async Task<IResult> LoginUser([FromBody] LoginDto loginDto, [FromServices] ISender mediator)
    {
        var res = await mediator.Send(new LoginCommand(loginDto));

        return res.IsSuccess ? Results.Ok(res.Value) : ApiResult.HandleError(res.Error);
    }
}