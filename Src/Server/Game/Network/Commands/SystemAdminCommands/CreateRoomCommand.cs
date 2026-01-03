public class CreateRoomCommand : ISystemAdminCommand
{
    public string? RoomName { get; set; }
    public CreateRoomCommand(string? roomName)
    {
        RoomName = roomName;
    }
}
