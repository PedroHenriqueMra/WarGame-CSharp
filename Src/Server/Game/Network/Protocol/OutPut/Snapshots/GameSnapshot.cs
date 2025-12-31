public record GameSnapshot
(
    long Tick,
    IReadOnlyList<PlayerSnapshot> Players
);
