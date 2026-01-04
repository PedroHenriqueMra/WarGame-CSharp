public class CreateRoomInput : ISystemAdminInput
{
    public InputGroup Group { get; } = InputGroup.System;
    public bool AllowPayload { get; } = true;
    
    public string? RoomName { get; set; }
    
    public ISystemAdminCommand? ToCommand(Guid userId)
    {
        // RoomAdmin:
        // aply rules
        if (RoomName == null)
            return null;
            
        return new CreateRoomCommand(RoomName, userId);
    }
}
