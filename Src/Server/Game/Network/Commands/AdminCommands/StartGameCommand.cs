public class StartGameCommand : IAdminCommand
{
    public int RoomId { get; set; }
    public StartGameCommand(int roomId)
    {
        RoomId = roomId;
    }
}
