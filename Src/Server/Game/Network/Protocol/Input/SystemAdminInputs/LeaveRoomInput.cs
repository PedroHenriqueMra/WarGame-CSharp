public class LeaveRoomInput : ISystemAdminInput
{
    public InputGroup Group { get; } = InputGroup.System;
    public bool AllowPayload { get; } = true;

    public Guid? RoomId { get; set; } 

    public ISystemAdminCommand? ToCommand(Guid userId)
    {
        if (RoomId == null)
            return null;

        return new LeaveRoomCommand(RoomId.Value, userId);
    }
}
