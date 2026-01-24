public record JumpInput : IGameplayInput
{
    public InputGroup Group { get; } = InputGroup.Gameplay;

    public bool AllowPayload { get; } = true;
    public int? InputTick { get; set; }

    public IGameplayCommand? ToCommand(Session session)
    {
        if (session.User.CurrentRoomId == null)
            return null;
        if (InputTick == null || InputTick == 0)
            return null;

        var inputTick = InputTick.Value;
            
        return new JumpPlayerCommand(session, inputTick);
    }
}
