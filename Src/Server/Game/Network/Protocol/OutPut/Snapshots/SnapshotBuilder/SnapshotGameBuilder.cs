public sealed class SnapshotGameBuilder : ISnapshotBuilder<GameSnapshot, Game>
{
    public GameSnapshot Build(Game game, long tick)
    {
        var players = game.GetPlayers()
        .Select(p => new PlayerSnapshot(
            p.Id,
            p.Position.X,
            p.Position.Y,
            p.DirectionX.DirectionX,
            p.IsGrounded)).ToList();

        return new GameSnapshot(tick, game.IsRunning, players);
    }
}
