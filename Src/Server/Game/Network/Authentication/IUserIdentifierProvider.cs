public interface IUserIdentifierProvider
{
    public bool TryGetUserId(HttpContext context, out Guid userId);
}
