public struct JoinPlayerDto
{
    public Room Room { get; set; }
    public Player player { get; set; }

    public JoinPlayerDto(Room room, Player player)
    {
        this.Room = room;
        this.player = player;
    }
}
