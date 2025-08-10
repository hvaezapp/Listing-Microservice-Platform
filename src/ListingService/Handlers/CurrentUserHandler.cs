using System.Security.Claims;

namespace ListingService.Handlers;

public class CurrentUserHandler
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public CurrentUserHandler(IHttpContextAccessor httpContextAccessor) => _httpContextAccessor = httpContextAccessor;
    public long UserId => Convert.ToInt64(_httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Sid));
}
