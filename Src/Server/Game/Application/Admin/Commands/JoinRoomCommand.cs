public class JoinRoomCommand : ISystemAdminCommand
{
    public int RoomId { get; set; }
    public Guid UserId { get; set; }
    public JoinRoomCommand(int roomId, Guid userId)
    {
        this.RoomId = roomId;
        this.UserId = userId;
    }    
}
