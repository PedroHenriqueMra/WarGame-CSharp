public record RoomStopResult
{
    public bool Status { get; private set; }
    public string Message { get; private set; }
    public RoomStopResult(bool status, string message)
    {
        Status = status;
        Message = message;
    }

    public static RoomStopResult Fail(string message)
        => new RoomStopResult(false, message);

    public static RoomStopResult Ok(string message = "Success")
        => new RoomStopResult(true, message);
}
