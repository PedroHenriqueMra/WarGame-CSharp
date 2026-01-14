public record RoomInfoDto
(
    Guid RoomId,
    string RoomName,
    int UserCount,
    bool IsRunning
);
