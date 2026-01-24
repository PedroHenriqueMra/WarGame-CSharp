public class MapGrid : IMapGame
{
    public float Width { get; private set; }
    public float Height { get; private set; }
    public Platform Platform { get; private set; }

    public MapGrid()
    {
        this.Width = 500f;
        this.Height = 500f;

        //this.Platform = new Platform(0f, 50f, 50f, 5f);
    }

    public bool IsInsideOfMap(float x, float y)
    {
        if (x < 0 || x > Width)
            return false;
        if (y < 0 || y > Height)
            return false;

        return true;
    }

    public bool IsWalkeble(float x, float y)
    {
        if (y == 0)
            return true;
        //if (Platform.IsOnTopOfPlatform(x, y))
        //    return true;

        return false;
    }
}
