public class LeaveRoomCommand : ISystemAdminCommand
{
    public int RoomId { get; set; }
    public Guid UserId { get; set; }

    public LeaveRoomCommand(int roomId, Guid userId)
    {
           RoomId = roomId;
           UserId = userId;
    }
}
