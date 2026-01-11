public class CreateRoomCommand : ISystemAdminCommand
{
    public string? RoomName { get; set; }
    public Guid UserId { get; set; }
    public CreateRoomCommand(string roomName, Guid userId)
    {
        RoomName = roomName;
        UserId = userId;
    }
}
