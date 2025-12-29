public struct Formulas
{
    public float Lerp(float a, float b, float t)
    {
        return a + (b - a) * t;
    } 

    public float MoveTowards(float current, float target, float maxDelta)
    {
        if (MathF.Abs(target - current) <= maxDelta)
            return target;

        return current + MathF.Sign(target - current) * maxDelta;
    }
}
