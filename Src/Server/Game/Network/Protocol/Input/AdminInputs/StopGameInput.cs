public class StopGameInput : IInput
{
    public InputGroup Group { get; } = InputGroup.Admin;
    public bool AllowPayload { get; } = true;

    public int? RoomId { get; set; }

    public IGameCommand? ToCommand(Session session)
    {
        if (RoomId == null)
            return null;

        return new StopGameCommand(RoomId.Value);
    }
}
