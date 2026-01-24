using System.Numerics;

public class PlayerPhysics
{
    private readonly PhysicsConfig _physicsConfig = new();
    private readonly Formulas _formulas = new();

    public float MoveHorizontal(Player pl, IMapGame map, float dt)
    {
        float accel = _physicsConfig.GetAccelerationForMaxSpeed(pl.Speed);
        float decel = _physicsConfig.GetDeceleration(pl.Speed);
        
        float deltaV;

        if (pl.PlayerIntentions.DirectionRequest.DirectionX == 0)
        {
            // if player is not moving, break
            deltaV = -MathF.Sign(pl.CurrentVelocity.X) * decel * dt;

            // prevent overshoot
            if (Math.Abs(pl.CurrentVelocity.X) <= decel * dt)
                deltaV = -pl.CurrentVelocity.X;
        }
        else if (pl.CurrentVelocity.X != 0 && MathF.Sign(pl.CurrentVelocity.X) != pl.PlayerIntentions.DirectionRequest.DirectionX)
        {
            // if player is changing direction, break
            deltaV = -MathF.Sign(pl.CurrentVelocity.X) * decel * dt;

            if (Math.Abs(pl.CurrentVelocity.X) <= decel * dt)
                deltaV = -pl.CurrentVelocity.X;
        }
        else
        {
            // Apply acceleration
            deltaV = pl.PlayerIntentions.DirectionRequest.DirectionX * accel * dt;
        }

        return deltaV;
    }

    public void ApplyVerticalPhysics(Player player, IMapGame map, float dt)
    {
        // Gravity
        player.CurrentVelocity = new Vector2(
            player.CurrentVelocity.X,
            GravityAction(player, dt)
        );

        float nextY = player.Position.Y + player.CurrentVelocity.Y;

        ResolveVerticalCollision(player, map, nextY);
    }

    private void ResolveVerticalCollision(Player player, IMapGame map, float nextY)
    {
        // Ground
        if (nextY <= 0)
        {
            player.Position.Y = 0;
            player.CurrentVelocity = new Vector2(player.CurrentVelocity.X, 0);
            player.IsGrounded = true;
            return;
        }

        // Roof
        if (nextY >= map.Height)
        {
            player.Position.Y = map.Height;
            player.CurrentVelocity = new Vector2(player.CurrentVelocity.X, 0);
            player.IsGrounded = false;
            return;
        }

        // Walkable surface
        if (map.IsWalkeble(player.Position.X, nextY))
        {
            player.Position.Y = nextY;
            player.IsGrounded = true;
        }
        else
        {
            player.Position.Y = nextY;
            player.IsGrounded = false;
        }
    }

    private float GravityAction(Player pl, float dt)
    {
        return pl.CurrentVelocity.Y + _physicsConfig._gravity * dt;
    }
}
