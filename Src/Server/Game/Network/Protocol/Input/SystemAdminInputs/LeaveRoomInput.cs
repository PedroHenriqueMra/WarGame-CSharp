public class LeaveRoomInput : IInput
{
    public InputGroup Group { get; } = InputGroup.System;
    public bool AllowPayload { get; } = true;

    public int? RoomId { get; set; } 

    public ICommand? ToCommand(Session session)
    {
        if (RoomId == null)
            return null;

        return new LeaveRoomCommand(RoomId.Value);
    }
}
