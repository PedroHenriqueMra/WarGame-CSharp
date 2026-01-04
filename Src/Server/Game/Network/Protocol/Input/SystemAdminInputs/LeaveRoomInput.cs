public class LeaveRoomInput : ISystemAdminInput
{
    public InputGroup Group { get; } = InputGroup.System;
    public bool AllowPayload { get; } = true;

    public int? RoomId { get; set; } 

    public ISystemAdminCommand? ToCommand(Guid userId)
    {
        if (RoomId == null)
            return null;

        return new LeaveRoomCommand(RoomId.Value, userId);
    }
}
