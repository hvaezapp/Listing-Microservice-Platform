using ListingService.Shared;

namespace ListingService.Middleware;

public class BusinessIdMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;
    public async Task InvokeAsync(HttpContext context)
    {
        if (!context.Request.Headers.TryGetValue("X-Business-Id", out var businessId) ||
            businessId != DefaultConstants.BusinessId)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsync("Access Denied!");
            return;
        }
        await _next(context);
    }
}
