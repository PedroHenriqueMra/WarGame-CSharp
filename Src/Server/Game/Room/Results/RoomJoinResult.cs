public record RoomJoinResult
{
    public bool Status { get; private set; }
    public string Message { get; private set; }
    public RoomJoinResult(bool status, string message)
    {
        Status = status;
        Message = message;
    }

    public static RoomJoinResult Fail(string message)
        => new RoomJoinResult(false, message);

    public static RoomJoinResult Ok(string message = "Success")
        => new RoomJoinResult(true, message);
}
