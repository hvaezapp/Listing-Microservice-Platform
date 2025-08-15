using IdentityService.Domain.Entities;
using IdentityService.Dtos.Token;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Shared.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IdentityService.Handlers;

public class JwtTokenHandler(IOptions<JwtSetting> jwtSetting)
{
    private readonly IOptions<JwtSetting> _jwtSetting = jwtSetting;

    public JwtTokenResponceDto GenerateToken(Claim[] claims)
    {
        var jwtSetting = _jwtSetting.Value;
        if (jwtSetting is null)
            throw new ArgumentNullException(nameof(jwtSetting), "jwtSetting not found!");

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSetting.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var expire = DateTime.Now.AddHours(1);

        var jwtSecurityToken = new JwtSecurityToken(

            issuer: jwtSetting.Issuer,
            audience: jwtSetting.Audience,
            claims: claims,
            expires: expire,
            signingCredentials: creds

        );

        var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        return new JwtTokenResponceDto(token, (int)Math.Ceiling((expire - DateTime.Now).TotalMinutes), expire);

    }
}
