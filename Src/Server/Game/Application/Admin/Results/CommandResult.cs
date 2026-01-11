public record CommandResult
{
    public bool Status { get; init; }
    public string Code { get; init; }
    public string Message { get; init; }
    public CommandResult(bool status, string code, string message)
    {
        Status = status;
        Code = code;
        Message = message;
    }

    public static CommandResult Ok(string code, string message = "Success")
        => new(true, code, message );
    public static CommandResult Fail(string message, string code)
        => new(false, code, message);
}

