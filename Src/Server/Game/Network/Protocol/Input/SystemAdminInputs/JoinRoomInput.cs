public class JoinRoomInput : ISystemAdminInput
{
    public InputGroup Group { get; } = InputGroup.System;
    public bool AllowPayload { get; } = true;

    public int? RoomId { get; set; }

    public ISystemAdminCommand? ToCommand(Guid userId)
    {
        if (RoomId == null)
            return null;
        if (userId == null)
            return null;

        var roomId = RoomId.Value;
        return new JoinRoomCommand(roomId, userId);
    }
}