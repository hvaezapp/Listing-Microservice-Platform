using ListingService.Middleware;

namespace ListingService.Shared;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseBusinessIdMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<BusinessIdMiddleware>();
    }

}
