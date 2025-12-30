public interface IInput
{
    public InputGroup Group { get; }
    public bool AllowPayload { get; }
    public IGameCommand? ToCommand(Session session);
}
