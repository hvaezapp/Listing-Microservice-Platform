using IdentityService.Domain.Entities;
using IdentityService.Dtos.Token;
using IdentityService.Dtos.User;
using IdentityService.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace IdentityService.Handlers;

public class AuthHandler(IdentityDBContext identityDBContext , JwtTokenHandler jwtTokenHandler)
{
    private readonly IdentityDBContext _identityDBContext = identityDBContext;
    private readonly JwtTokenHandler _jwtTokenHandler = jwtTokenHandler;

    public async Task<JwtTokenResponceDto> Login(LoginRequestDto dto, CancellationToken cancellationToken)
    {
        // check some conditions
        var user = await _identityDBContext.Users
                                            .FirstOrDefaultAsync(a => a.Username.Equals(dto.username) &&
                                             a.Password.Equals(dto.password) ,
                                             cancellationToken);
        if (user is null)
            throw new ArgumentNullException(nameof(user) , "user not found!");


        // get claim from user claims dynamically
        var claims = new[]
        {
           new Claim(ClaimTypes.Sid, user.Id.ToString()),
           new Claim(ClaimTypes.NameIdentifier , user.Username)
        };

        return _jwtTokenHandler.GenerateToken(claims);
    }


    public async Task Register(RegisterReqeustDto dto  , CancellationToken cancellationToken)
    {
        // check some conditions

        var newUser = User.Create(dto.firstName , dto.lastName,
                                  dto.userName , dto.password);

        await _identityDBContext.Users.AddAsync(newUser , cancellationToken);
        await _identityDBContext.SaveChangesAsync(cancellationToken);
    }
}
