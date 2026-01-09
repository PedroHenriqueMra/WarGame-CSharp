public sealed class UserIdentifierProvider : IUserIdentifierProvider
{
    public bool TryGetUserId(HttpContext context, out Guid userId)
    {
        userId = default;

        if (!context.Request.Cookies.TryGetValue("dev-auth", out var token))
            return false;

        return Guid.TryParse(token, out userId);
    }
}