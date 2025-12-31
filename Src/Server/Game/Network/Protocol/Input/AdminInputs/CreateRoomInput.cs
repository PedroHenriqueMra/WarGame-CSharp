using System.Text.Json;

public class CreateRoomInput : IInput
{
    public InputGroup Group { get; } = InputGroup.Admin;
    public bool AllowPayload { get; } = true;
    
    public string? RoomName { get; set; }
    
    public IGameCommand? ToCommand(Session session)
    {
        // RoomAdmin:
        // aply rules
        if (session.User.UserId == null)
            return null;
        if (RoomName == null)
            return null;

        var userId = session.User.UserId; 
        if (!RoomAdmin.CanCreateRoom(new CreateRoomDto(userId, RoomName)))
            return null;
            
        return new CreateRoomCommand(RoomName);
    }
}
