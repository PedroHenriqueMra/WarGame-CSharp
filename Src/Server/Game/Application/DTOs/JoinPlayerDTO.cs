public struct JoinPlayerDTO
{
    public Room Room { get; set; }
    public Player player { get; set; }

    public JoinPlayerDTO(Room room, Player player)
    {
        this.Room = room;
        this.player = player;
    }
}
