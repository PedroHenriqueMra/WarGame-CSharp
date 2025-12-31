using System.Text.Json;

public record JumpInput : IInput
{
    public InputGroup Group { get; } = InputGroup.Gameplay;

    public bool AllowPayload { get; } = false;

    public IGameCommand? ToCommand(Session session)
    {
        if (session.User.CurrentRoomId == null)
            return null;
            
        return new JumpPlayerCommand(session);
    }
}
