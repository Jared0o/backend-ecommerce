using Ecommerce.Commons.Abstraction.Helpers;
using Ecommerce.Modules.Users.Core.Commands.AddUser;
using Mediator;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Ecommerce.Modules.Users.Api.Routes;

public static class UserEndpoints
{
    public static RouteGroupBuilder MapUserEndpoints(this RouteGroupBuilder group)
    {
        group.MapPost("/register", CreateUser);
        group.MapGet("/test", () => "Hello World");

        return group;
    }

    private static async Task<Results<NoContent, BadRequest<Error>, NotFound<Error>>> CreateUser([FromBody] RegisterUserDto userDto, [FromServices] ISender mediator)
    {
        var res = await mediator.Send(new AddUserCommand(userDto));

        if (res.IsSuccess)
        {
            return TypedResults.NoContent();
        }

        return res.Error?.Type switch
        {
            ErrorType.NotFound => TypedResults.NotFound(res.Error),
            ErrorType.ValidationError => TypedResults.BadRequest(res.Error),
            ErrorType.Exception => TypedResults.BadRequest(res.Error),
            _ => TypedResults.BadRequest(res.Error)
        };
    }
}