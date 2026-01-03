public class StartGameCommand : IGameAdminCommand
{
    public int RoomId { get; set; }
    public StartGameCommand(int roomId)
    {
        RoomId = roomId;
    }
}
