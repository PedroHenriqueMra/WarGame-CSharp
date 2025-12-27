
public class Player
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    
    // Gameplay
    public float Speed { get; set; }
    public PlayerPosition Position { get; set; }

    
    public Player (int id, string name)
    {
        this.Id = id;
        this.Name = name;

        this.Speed = 5f;
        this.Position = new PlayerPosition();
    }

    public void Move(float dx, float dy)
    {
        this.Position.X += dx;
        this.Position.Y += dy;
    }
}

public class PlayerPosition()
{
    public float X { get; set; } = 0f;
    public float Y { get; set; } = 0f;
}
