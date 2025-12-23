public struct CreateRoomDTO
{
    public string RoomName { get; set; }
    public int AdminId { get; set; }
    public CreateRoomDTO(int adminId, string roomName)
    {
        this.AdminId = adminId;
        this.RoomName = roomName;
    }
}
