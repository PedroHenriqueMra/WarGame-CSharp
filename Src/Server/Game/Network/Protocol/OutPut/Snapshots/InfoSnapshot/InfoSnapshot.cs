public class InfoSnapshot : IPayload
{
    public bool Success { get; set; }
    public string Code { get; set; }
    public string Message { get; set; }
    public object? Content { get; set; }
    public InfoSnapshot(bool success, string code, string message, object content = null)
    {
        Success = success;
        Code = code;
        Message = message;
        Content = content;
    }
}
