using System.Numerics;

public class PlayerPhysics : IPhysicsEngine<Player>
{
    private readonly PhysicsConfig _physicsConfig = new();
    private readonly Formulas _formulas = new();

    public float MoveHorizontal(Player pl, IMapGame map, float dt)
    {
        // if player is not moving, reset velocity
        if (pl.DirectionX.DirectionX == 0 )
        {
            return 0f;
        }

        // if player is changing direction, reset velocity
        if (pl.CurrentVelocity.X != 0 && MathF.Sign(pl.CurrentVelocity.X) != pl.DirectionX.DirectionX)
            return 0f;

        // Apply acceleration
        float targetVelocity = pl.DirectionX.DirectionX * pl.Speed;
        float acceleration = _physicsConfig.GetAceleration(pl.Speed);

        return _formulas.MoveTowards (
            pl.CurrentVelocity.X,
            targetVelocity,
            acceleration * dt
        );
    }

    //private float Gravity()
    //{
    //    
    //}
}
