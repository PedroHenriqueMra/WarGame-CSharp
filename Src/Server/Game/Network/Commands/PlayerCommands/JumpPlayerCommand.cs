using System.Numerics;

public record JumpPlayerCommand : IGameplayCommand
{
    public Session Session { get; set; }
    public int InputTick { get; set; }
    public JumpPlayerCommand(Session session, int inputTick)
    {
        this.Session = session;
        this.InputTick = inputTick;
    }
    
    public void Execute (Game game)
    {
        var player = game.GetPlayerByUserId(Session.User.UserId);
        if (player == null)
            return;

        player.PlayerIntentions.JumpRequest = true;
    }
}
