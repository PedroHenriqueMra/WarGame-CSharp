public interface IMapGame
{
    public float Width { get; }
    public float Height { get; }
    public bool IsInsideOfMap(float x, float y);
    public bool IsWalkeble(float x, float y);
}
