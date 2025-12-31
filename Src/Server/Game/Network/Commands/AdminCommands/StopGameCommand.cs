public class StopGameCommand : IAdminCommand
{
    public int RoomId { get; set; }
    public StopGameCommand(int roomId)
    {
        RoomId = roomId;
    }
}
