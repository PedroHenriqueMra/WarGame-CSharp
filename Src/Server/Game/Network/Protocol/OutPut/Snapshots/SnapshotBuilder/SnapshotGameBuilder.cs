public sealed class SnapshotGameBuilder : ISnapshotBuilder
{
    public GameSnapshot Build(Game game, long tick)
    {
        var players = game.GetPlayers()
        .Select(p => new PlayerSnapshot(
            p.Id,
            p.Position.X,
            p.Position.Y,
            p.CurrentVelocity.X,
            p.CurrentVelocity.Y,
            p.IsGrounded)).ToList();

        return new GameSnapshot(tick, players);
    }
}
