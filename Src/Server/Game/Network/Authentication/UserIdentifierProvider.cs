public sealed class UserIdentifierProvider : IUserIdentifierProvider
{
    public bool TryGetUserId(HttpContext context, out Guid userId)
    {
        userId = default;

        if (context.Request.Headers.TryGetValue("token-t", out var token))
            return false;

        return Guid.TryParse(token, out userId);
    }
}