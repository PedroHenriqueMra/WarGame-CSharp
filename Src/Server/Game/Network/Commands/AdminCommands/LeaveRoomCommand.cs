public class LeaveRoomCommand : IAdminCommand
{
    public int RoomId { get; set; }

    public LeaveRoomCommand(int roomId)
    {
           RoomId = roomId;
    }
}
