public record MovePlayerCommand : IGameplayCommand
{
    public Session Session { get; set; }
    public int InputTick { get; set; }
    private int Direction;
    public MovePlayerCommand(Session session, int direction, int inputTick)
    {
        this.Session = session;
        this.Direction = direction;
        this.InputTick = inputTick;
    }
    
    public void Execute (Game game)
    {
        var player = game.GetPlayerByUserId(Session.User.UserId);
        if (player == null)
            return;
            
        player.PlayerIntentions.DirectionRequest.ChangeDirection(Direction);
    }
}
