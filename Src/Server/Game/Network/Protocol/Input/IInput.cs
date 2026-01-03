public interface IInput
{
    public InputGroup Group { get; }
    public bool AllowPayload { get; }
    public ICommand? ToCommand(Session session);
}
