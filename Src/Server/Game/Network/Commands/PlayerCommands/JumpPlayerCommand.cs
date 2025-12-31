using System.Numerics;

public record JumpPlayerCommand : IGameplayCommand
{
    public Session Session { get; set; }
    public JumpPlayerCommand(Session session)
    {
        this.Session = session;
    }
    
    public void Execute (Game game)
    {
        var player = game.GetPlayerByUserId(Session.User.UserId);
        if (player == null)
            return;

        player.JumpRequest = true;
    }
}
