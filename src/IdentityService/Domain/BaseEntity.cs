namespace IdentityService.Domain;

public class BaseEntity<T>
{
    public T Id { get;  private set; }

}
public abstract class BaseDomainEntity : BaseEntity<Guid> { }
