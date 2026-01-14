public class StopGameCommand : IGameAdminCommand
{
    public Guid RoomId { get; set; }
    public StopGameCommand(Guid roomId)
    {
        RoomId = roomId;
    }
}
