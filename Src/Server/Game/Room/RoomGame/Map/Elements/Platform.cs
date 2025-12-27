public class Platform
{
    public float PosX { get; set; }
    public float PosY { get; set; }
    public float Width { get; set; }
    public float Height { get; set; }

    public Platform(float posX, float posY, float width, float height)
    {
        this.PosX = posX;
        this.PosY = posY;
        this.Width = width;
        this.Height = height;
    }

    public bool IsOnTopOfPlatform(float x, float y)
    {
        if (y == this.PosY && x >= this.PosX && x <= this.PosX + this.Width)
            return true;

        return false;
    }
}
