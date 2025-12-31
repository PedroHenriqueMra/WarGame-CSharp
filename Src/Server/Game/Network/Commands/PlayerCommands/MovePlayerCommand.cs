using System.Numerics;

public record MovePlayerCommand : IGameplayCommand
{
    public Session Session { get; set; }
    private int Direction;
    public MovePlayerCommand(Session session, int direction)
    {
        this.Session = session;
        this.Direction = direction;
    }
    
    public void Execute (Game game)
    {
        var player = game.GetPlayerByUserId(Session.User.UserId);
        if (player == null)
            return;
            
        player.DirectionX.ChangeDirection(Direction);
    }
}
