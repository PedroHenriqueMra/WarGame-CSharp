public sealed class SnapshotGameBuilder : ISnapshotBuilder<GameSnapshot, Game>
{
    public GameSnapshot Build(Game game, long tick, Session receiver)
    {
        var players = game.GetPlayers()
        .Select(p => new PlayerSnapshot(
            p.Id,
            p.Position.X,
            p.Position.Y,
            p.CurrentVelocity.X,
            p.CurrentVelocity.Y,
            p.DirectionX.DirectionX,
            p.IsGrounded,
            PlayerAttributes: new
            {
                p.Health,
                p.Speed,
                p.JumpForce
            }
            )).ToList();

        var localUser = receiver.User;
        var lastReceivedInputTick = game.GetPlayerByUserId(localUser.UserId)!.LastReceivedInputTick;

        return new GameSnapshot(tick, game.IsRunning, players, lastReceivedInputTick);
    }
}
