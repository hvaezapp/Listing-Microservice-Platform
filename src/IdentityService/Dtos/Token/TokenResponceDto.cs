namespace IdentityService.Dtos.Token;

public record TokenResponseDto(string token , int expireOnMin , DateTime expireDatetime);
