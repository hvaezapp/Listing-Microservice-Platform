using IdentityService.Dtos.User;
using IdentityService.Handlers;

namespace IdentityService.Endpoints;

public static class Endpoints
{
    public static void MapAuthEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapPost("/register", async (RegisterReqeustDto dto , 
                                     AuthHandler authHandler , 
                                     CancellationToken cancellationToken) =>
        {
            await authHandler.Register(dto, cancellationToken);
            return Results.Ok();
        });


        builder.MapPost("/login", async (LoginRequestDto dto,
                                         AuthHandler authHandler,
                                         CancellationToken cancellationToken) =>
        {
            return Results.Ok(await authHandler.Login(dto, cancellationToken));
        });


    }
}
