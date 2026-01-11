public record SystemAdminResult
{
    public bool Status { get; init; }
    public string Code { get; init; }
    public string Message { get; init; }
    public SystemAdminResult(bool status, string code, string message)
    {
        Status = status;
        Code = code;
        Message = message;
    }

    public static SystemAdminResult Ok(string code, string message = "Success")
        => new(true, code, message );
    public static SystemAdminResult Fail(string message, string code)
        => new(false, code, message);
}

