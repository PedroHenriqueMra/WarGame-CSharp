using System.Reflection.Metadata.Ecma335;

public class PhysicsConfig
{
    public float _gravity { get; } = 9.8f;
    private readonly float _horizontalDeceleration = 1f;
    public float GetDeceleration(float maxSpeed)
        => maxSpeed / _horizontalDeceleration;
    
    private readonly float _horizontalAceleration = 3f;
    public float GetAccelerationForMaxSpeed(float maxSpeed)
        => maxSpeed / _horizontalAceleration;
}
