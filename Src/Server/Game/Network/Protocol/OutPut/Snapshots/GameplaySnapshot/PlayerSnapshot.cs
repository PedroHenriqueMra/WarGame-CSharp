public record PlayerSnapshot
(
    int PlayerId,
    float X,
    float Y,
    float VelocityX,
    float VelocityY,
    int Dir,
    bool IsGrounded,
    object PlayerAttributes
);
