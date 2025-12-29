using System.Reflection.Metadata.Ecma335;

public class PhysicsConfig
{
    private readonly float _gravity = 9.8f;
    private readonly float _horizontalAceleration = 3f;
    public float GetAceleration(float maxSpeed)
        => maxSpeed / _horizontalAceleration;
}
