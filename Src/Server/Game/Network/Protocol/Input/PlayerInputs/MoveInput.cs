using NLog.LayoutRenderers;

public record MoveInput : IInput
{
    public InputGroup Group { get; } = InputGroup.Gameplay;

    public bool AllowPayload { get; } = true;
    public int? Direction { get; set; } = default;

    public IGameCommand? ToCommand(Session session)
    {
        if (session.User.UserId == null)
            return null;
        if (session.User.CurrentRoomId == null)
            return null;
        if (Direction == null)
            return null;

        var userId = session.User.UserId;
        var direction = Direction!.Value;

        return new MovePlayerCommand(userId, direction);
    }
}
