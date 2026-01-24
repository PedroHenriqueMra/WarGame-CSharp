public record MoveInput : IGameplayInput
{
    public InputGroup Group { get; } = InputGroup.Gameplay;

    public bool AllowPayload { get; } = true;
    public int? InputTick { get; set; }
    public int? Direction { get; set; } = default;

    public IGameplayCommand? ToCommand(Session session)
    {
        if (session.User.CurrentRoomId == null)
            return null;
        if (Direction == null)
            return null;
        if (InputTick == null || InputTick == 0)
            return null;

        var direction = Direction!.Value;
        var inputTick = InputTick!.Value;

        return new MovePlayerCommand(session, direction, inputTick);
    }
}
