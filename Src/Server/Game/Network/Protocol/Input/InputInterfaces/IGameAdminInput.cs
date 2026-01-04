public interface IGameAdminInput
{
    public InputGroup Group { get; }
    public bool AllowPayload { get; }
    public IGameAdminCommand? ToCommand(Session session);
}