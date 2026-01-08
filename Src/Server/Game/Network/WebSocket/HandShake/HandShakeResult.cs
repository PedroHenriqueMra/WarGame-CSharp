using Microsoft.Extensions.Diagnostics.HealthChecks;

public struct HandShakeResult
{
    public StatusHandShakeResult Status { get; private set; }
    public string Message { get; private set; }
    public object? Content { get; private set; }

    public HandShakeResult(StatusHandShakeResult status, string message, object? content = null)
    {
        Status = status;
        Message = message;
        Content = content;
    }

    public static HandShakeResult Success(object content, string message = "Success")
        => new HandShakeResult(StatusHandShakeResult.success, message, content);
        
    public static HandShakeResult Failed(string message = "Failed")
        => new HandShakeResult(StatusHandShakeResult.failed, message);
}

public enum StatusHandShakeResult
{
    success,
    failed
}
