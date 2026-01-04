public class StartGameInput : IGameAdminInput
{
    public InputGroup Group { get; } = InputGroup.Admin;
    public bool AllowPayload { get; } = true;

    public int? RoomId { get; set; }

    public IGameAdminCommand? ToCommand(Session session)
    {
        if (RoomId is null)
            return null;

        return new StartGameCommand(RoomId.Value);
    }
}