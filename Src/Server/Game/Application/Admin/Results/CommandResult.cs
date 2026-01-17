public record CommandResult
{
    public bool Status { get; init; }
    public string Code { get; init; }
    public string Message { get; init; }
    public object? Content { get; set; }
    public CommandResult(bool status, string code, string message, object content = null)
    {
        Status = status;
        Code = code;
        Message = message;
        Content = content;
    }

    public static CommandResult Ok(string code, string message = "Success", object content = null)
        => new(true, code, message, content);
    public static CommandResult Fail(string message, string code, object content = null)
        => new(false, code, message, content);
}

