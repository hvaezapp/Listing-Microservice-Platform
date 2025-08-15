namespace Shared.Auth;

public class JwtSetting
{
    public const string Name = "JwtSetting";
    public string? Key { get; set; }
    public string? Issuer { get; set; }
    public string? Audience { get; set; }
}
