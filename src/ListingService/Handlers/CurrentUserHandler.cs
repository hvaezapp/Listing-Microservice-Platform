using System.Security.Claims;

namespace ListingService.Handlers;

public class CurrentUserHandler
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public CurrentUserHandler(IHttpContextAccessor httpContextAccessor) => _httpContextAccessor = httpContextAccessor;
    public Guid UserId => Guid.Parse(_httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Sid));
}
