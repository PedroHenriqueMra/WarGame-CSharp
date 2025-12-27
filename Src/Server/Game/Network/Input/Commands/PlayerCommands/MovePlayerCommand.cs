public record MovePlayerCommand(int PlayerId, int MoveX, int MoveY) : IGameCommand
{
    public void Execute (Game game, float deltaTime)
    {
        if (game.Players.Any(x => x.Id == PlayerId))
        {
            Player player = game.Players.First(x => x.Id == PlayerId);
            float dx = MoveX * player.Speed * deltaTime;
            float dy = MoveY * player.Speed * deltaTime;

            // after rules of colision, can jump, can move, etc

            player.Move(dx, dy);
        }
    }
}
