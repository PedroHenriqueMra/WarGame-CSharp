public record RoomStartResult
{
    public bool Status { get; private set; }
    public string Message { get; private set; }
    public RoomStartResult(bool status, string message)
    {
        Status = status;
        Message = message;
    }

    public static RoomStartResult Fail(string message)
        => new RoomStartResult(false, message);

    public static RoomStartResult Ok(string message = "Success")
        => new RoomStartResult(true, message);
}
