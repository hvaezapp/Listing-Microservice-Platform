using Shared.Domain;

namespace IdentityService.Domain.Entities;

public class User : BaseDomainEntity
{
    public string Firstname { get; private set; } = null!;
    public string Lastname { get; private set; } = null!;
}
