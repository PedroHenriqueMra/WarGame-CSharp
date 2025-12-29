
using System.Data;
using System.Numerics;

public class Player
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    
    // Gameplay
    public float Heath { get; private set; }

    public float Speed { get; set; } // maximum speed capacity
    public float JumpForce { get; set; }
    public Vector2 CurrentVelocity { get; set; } // Current pixel by second player walks

    public PlayerDirectionX DirectionX { get; set; }
    public PlayerPosition Position { get; set; }

    // intentions
    public bool IsGrounded { get; set; }
    public bool JumpRequest { get; set; }

    
    public Player (int id, string name)
    {
        this.Id = id;
        this.Name = name;

        this.Speed = 15f;
        this.JumpForce = 10f;
        this.CurrentVelocity = new Vector2(0f, 0f);
        this.Position = new PlayerPosition();
        this.DirectionX = new PlayerDirectionX();

        this.Heath = 100f;
        this.IsGrounded = true;
        this.JumpRequest = false;
    }

    public void Update()
    {
        ClearIntentions();
    } 

    private void ClearIntentions()
    {
        this.JumpRequest = false;
    }
}

public class PlayerPosition()
{
    public float X { get; set; } = 0f;
    public float Y { get; set; } = 0f;
}

public class PlayerDirectionX
{
    public int DirectionX { get; private set; } = 0;

    public void ChangeDirection(int x)
    {
        DirectionX = Math.Clamp(x, -1, 1);
    }
}
