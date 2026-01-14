public class StartGameCommand : IGameAdminCommand
{
    public Guid RoomId { get; set; }
    public StartGameCommand(Guid roomId)
    {
        RoomId = roomId;
    }
}
