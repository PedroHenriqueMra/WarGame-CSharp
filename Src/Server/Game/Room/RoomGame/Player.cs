using System.Numerics;

public class Player
{
    public int Id { get; private set; }
    public Guid UserId { get; set; }
    public string Name { get; set; }
    
    // Gameplay
    public float Health { get; private set; }

    public float Speed { get; set; } // maximum speed capacity
    public float JumpForce { get; set; }
    public Vector2 CurrentVelocity { get; set; } // Current pixel by second player walks

    public PlayerDirectionX DirectionX { get; set; }
    public PlayerPosition Position { get; set; }

    public bool IsGrounded { get; set; }

    public int LastReceivedInputTick { get; set; }
    public PlayerIntentions PlayerIntentions { get; set; }
    
    public Player (int id, Guid userId, string name)
    {
        this.Id = id;
        this.UserId = userId;
        this.Name = name;

        this.Speed = 100f;
        this.JumpForce = 15f;
        this.CurrentVelocity = new Vector2(0f, 0f);
        this.Position = new PlayerPosition();

        this.DirectionX = new PlayerDirectionX();

        this.Health = 100f;
        this.IsGrounded = true;
        this.PlayerIntentions = new();
        this.LastReceivedInputTick = 0;
    }

    public void Update()
    {
        ClearIntentions();
    } 

    private void ClearIntentions()
    {
        this.PlayerIntentions.JumpRequest = false;
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

public class PlayerIntentions
{
    // Resetable
    public bool JumpRequest { get; set; }

    // No resetable
    public PlayerDirectionX DirectionRequest { get; set; } = new();
}
