public class JoinRoomCommand : ISystemAdminCommand
{
    public Guid RoomId { get; set; }
    public Guid UserId { get; set; }
    public JoinRoomCommand(Guid roomId, Guid userId)
    {
        this.RoomId = roomId;
        this.UserId = userId;
    }    
}
