public interface IGameplayInput
{
    public InputGroup Group { get; }
    public bool AllowPayload { get; }
    public IGameplayCommand? ToCommand(Session session);
}
