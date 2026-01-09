public class InfoSnapshot : IPayload
{
    public bool IsError { get; set; }
    public string Code { get; set; }
    public string Message { get; set; }
    public InfoSnapshot(bool isError, string code, string message)
    {
        IsError = isError;
        Code = code;
        Message = message;
    }
}
