
using System.Data;
using System.Numerics;

public class Player
{
    public int Id { get; private set; }
    public Guid UserId { get; set; }
    public string Name { get; set; }
    
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

    
    public Player (int id, Guid userId, string name)
    {
        this.Id = id;
        this.UserId = userId;
        this.Name = name;

        this.Speed = 8f;
        this.JumpForce = 5f;
        this.CurrentVelocity = new Vector2(0f, 0f);
        this.Position = new PlayerPosition();
        // DEBUG
        //Position.Y = 50f;

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
        DirectionX = Math.Sign(x);
    }
}
