public class RoomRestrictions
{
    public int MaxPlayers { get; }
    public int MinPlayers { get; }
    public int MaxRounds { get; }
    public int MinRounds { get; }
    public int MaxTime { get; }
    
    public RoomRestrictions()
    {
        this.MaxPlayers = 2;
        this.MinPlayers = 2;
        this.MaxRounds = 10;
        this.MinRounds = 1;
        this.MaxTime = 600;
    }

    public bool CanJoin(int currentPlayers)
    {
        return currentPlayers < this.MaxPlayers;
    }

    public bool CanInitRound(int rounds)
    {
        return rounds < this.MaxRounds;
    }
}
