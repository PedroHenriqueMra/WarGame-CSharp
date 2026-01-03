using NLog.LayoutRenderers;

public record MoveInput : IInput
{
    public InputGroup Group { get; } = InputGroup.Gameplay;

    public bool AllowPayload { get; } = true;
    public int? Direction { get; set; } = default;

    public ICommand? ToCommand(Session session)
    {
        if (session.User.CurrentRoomId == null)
            return null;
        if (Direction == null)
            return null;

        var direction = Direction!.Value;

        return new MovePlayerCommand(session, direction);
    }
}
