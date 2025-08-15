using Shared.Domain;

namespace IdentityService.Domain.Entities;

public class User : BaseDomainEntity
{
    public string Firstname { get; private set; } = null!;
    public string Lastname { get; private set; } = null!;
    public string Username { get; private set; } = null!;
    public string Password { get; private set; } = null!;
}
