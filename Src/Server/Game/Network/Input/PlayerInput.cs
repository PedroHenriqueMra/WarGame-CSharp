public class PlayerInput
{
    public int MoveX { get; set; } = default;
    public int MoveY { get; set; } = default;

    public PlayerInput (int moveX=0, int moveY=0)
    {
        this.MoveX = Math.Sign(moveX);
        this.MoveY = Math.Sign(moveY);
    }
}
