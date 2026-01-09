using System.Security.Claims;

public class DevAuthMiddleware
{
    private readonly RequestDelegate _next;   
    public DevAuthMiddleware (RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Cookies.TryGetValue("dev-auth", out var token) && Guid.TryParse(token, out Guid userid))
        {
            var userIdentity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.NameIdentifier, userid.ToString()) },
            "DevAuth"
            );

            context.User = new ClaimsPrincipal(userIdentity);
        }

        await _next(context);
    }
}
