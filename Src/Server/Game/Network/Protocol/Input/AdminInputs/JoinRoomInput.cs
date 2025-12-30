public class JoinRoomInput : IInput
{
    public InputGroup Group { get; } = InputGroup.Admin;
    public bool AllowPayload { get; } = true;

    public int? RoomId { get; set; }

    public IGameCommand? ToCommand(Session session)
    {
        if (RoomId == null)
            return null;
        if (session.User.UserId == null)
            return null;
        if (session.User.CurrentRoomId != null)
            return null;

        var roomId = RoomId.Value;
        return new JoinRoomCommand(roomId, session.User.UserId);
    }
}