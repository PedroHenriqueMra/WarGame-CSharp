public class LeaveRoomCommand : ISystemAdminCommand
{
    public Guid RoomId { get; set; }
    public Guid UserId { get; set; }

    public LeaveRoomCommand(Guid roomId, Guid userId)
    {
           RoomId = roomId;
           UserId = userId;
    }
}
