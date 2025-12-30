public struct CreateRoomDto
{
    public string RoomName { get; set; }
    public Guid UserId { get; set; }
    public CreateRoomDto(Guid UserId, string roomName)
    {
        this.UserId = UserId;
        this.RoomName = roomName;
    }
}
