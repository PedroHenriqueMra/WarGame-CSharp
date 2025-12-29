using System.Numerics;

public record JumpPlayerCommand(int PlayerId) : IGameCommand
{
    public void Execute (Game game)
    {
        Player? player = game.Players.FirstOrDefault(x => x.Id == PlayerId);
        if (player == null)
            return; 

        player.JumpRequest = true;
    }
}
