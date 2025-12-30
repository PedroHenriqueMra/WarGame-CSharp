public class JoinRoomCommand : IAdminCommand
{
    public int RoomId { get; set; }
    public Guid UserId { get; set; }
    public JoinRoomCommand(int roomId, Guid userId)
    {
        this.RoomId = roomId;
        this.UserId = userId;
    }    
}
