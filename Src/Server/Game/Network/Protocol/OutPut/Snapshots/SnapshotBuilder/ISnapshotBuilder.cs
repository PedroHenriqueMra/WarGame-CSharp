public interface ISnapshotBuilder<out TSnapshot, in TData>
{
    public TSnapshot Build(TData game, long tick, Session receiver);
}
