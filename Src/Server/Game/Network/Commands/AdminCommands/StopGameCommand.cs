public class StopGameCommand : IGameAdminCommand
{
    public int RoomId { get; set; }
    public StopGameCommand(int roomId)
    {
        RoomId = roomId;
    }
}
