public interface IGameplayInput
{
    public InputGroup Group { get; }
    public bool AllowPayload { get; }
    public int? InputTick { get; set; }
    public IGameplayCommand? ToCommand(Session session);
}
