public interface ISnapshotBuilder
{
    public GameSnapshot Build(Game game, bool isRunning, long tick);
}
