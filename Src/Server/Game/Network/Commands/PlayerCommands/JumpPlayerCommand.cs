using System.Numerics;

public record JumpPlayerCommand : IGameplayCommand
{
    private Guid? UserId;
    public JumpPlayerCommand(Session session)
    {
        this.UserId = session.User.UserId;
    }
    
    public void Execute (Game game)
    {
        if (UserId == null)
            return;

        var player = game.GetPlayerByUserId(UserId.Value);
        if (player == null)
            return;

        player.JumpRequest = true;
    }
}
