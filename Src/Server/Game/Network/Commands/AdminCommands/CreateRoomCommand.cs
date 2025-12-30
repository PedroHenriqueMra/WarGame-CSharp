public class CreateRoomCommand : IAdminCommand
{
    public string? RoomName { get; set; }
    public CreateRoomCommand(string? roomName)
    {
        RoomName = roomName;
    }
}
