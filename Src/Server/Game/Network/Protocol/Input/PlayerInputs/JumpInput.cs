using System.Text.Json;

public record JumpInput : IInput
{
    public InputGroup Group { get; } = InputGroup.Gameplay;

    public bool AllowPayload { get; } = false;

    public IGameCommand? ToCommand(Session session)
    {
        return new JumpPlayerCommand(session);
    }
}
