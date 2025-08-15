namespace IdentityService.Dtos.Token;

public record JwtTokenResponceDto(string token , int expireOnMin , DateTime expireDatetime);
