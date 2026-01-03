public class LeaveRoomCommand : ISystemAdminCommand
{
    public int RoomId { get; set; }

    public LeaveRoomCommand(int roomId)
    {
           RoomId = roomId;
    }
}
