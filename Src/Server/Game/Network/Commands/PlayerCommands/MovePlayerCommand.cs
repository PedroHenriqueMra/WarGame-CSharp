using System.Numerics;

public record MovePlayerCommand : IGameplayCommand
{
    private Guid UserId;
    private int Direction;
    public MovePlayerCommand(Guid userId, int direction)
    {
        this.UserId = userId;
        this.Direction = direction;
    }
    
    public void Execute (Game game)
    {
        var player = game.GetPlayerByUserId(UserId);
        if (player == null)
            return;
            
        player.DirectionX.ChangeDirection(Direction);
    }
}
