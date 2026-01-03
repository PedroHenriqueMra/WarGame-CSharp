public class StartGameInput : IInput
{
    public InputGroup Group { get; } = InputGroup.Admin;
    public bool AllowPayload { get; } = true;

    public int? RoomId { get; set; }

    public ICommand? ToCommand(Session session)
    {
        if (RoomId is null)
            return null;

        return new StartGameCommand(RoomId.Value);
    }
}