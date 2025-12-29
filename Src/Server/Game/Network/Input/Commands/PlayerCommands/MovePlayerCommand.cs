using System.Numerics;

public record MovePlayerCommand(int PlayerId, int Direction) : IGameCommand
{
    public void Execute (Game game, float deltaTime)
    {
        Player? player = game.Players.FirstOrDefault(x => x.Id == PlayerId);
        if (player == null)
            return; 

        player.DirectionX.ChangeDirection(Direction);
    }
}
