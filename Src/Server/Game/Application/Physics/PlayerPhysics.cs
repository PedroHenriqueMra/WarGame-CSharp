using System.Numerics;
using Microsoft.AspNetCore.Mvc.TagHelpers;

public class PlayerPhysics
{
    private readonly PhysicsConfig _physicsConfig = new();
    private readonly Formulas _formulas = new();

    public float MoveHorizontal(Player pl, IMapGame map, float dt)
    {
        // if player is not moving, reset velocity
        if (pl.DirectionX.DirectionX == 0)
        {
            float decel = _physicsConfig.GetDeceleration(pl.Speed);
            return _formulas.MoveTowards(pl.CurrentVelocity.X, 0, decel * dt);
        }

        // if player is changing direction, reset velocity
        if (pl.CurrentVelocity.X != 0 && MathF.Sign(pl.CurrentVelocity.X) != pl.DirectionX.DirectionX)
        {
            float decel = _physicsConfig.GetDeceleration(pl.Speed);
            return _formulas.MoveTowards(pl.CurrentVelocity.X, 0, decel * dt);
        }

        // Apply acceleration
        float targetVelocity = pl.DirectionX.DirectionX * pl.Speed;
        float acceleration = _physicsConfig.GetAccelerationForMaxSpeed(pl.Speed);

        return _formulas.MoveTowards(
            pl.CurrentVelocity.X,
            targetVelocity,
            acceleration * dt
        );
    }

    public void ApplyVerticalPhysics(Player player, IMapGame map, float dt)
    {
        // Gravity
        player.CurrentVelocity = new Vector2(
            player.CurrentVelocity.X,
            GravityAction(player, dt)
        );

        float nextY = player.Position.Y + player.CurrentVelocity.Y * dt;

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

    //public float Jump(Player pl, IMapGame map, float dt)
    //{
    //    return pl.JumpForce;
    //}

    private float GravityAction(Player pl, float dt)
    {
        return pl.CurrentVelocity.Y + _physicsConfig._gravity * dt;
    }
}
