public record CommandResult
{
    public bool Success { get; init; }
    public string Code { get; init; }
    public string? ErrorMessage { get; init; }

    public static CommandResult Ok(string code) => new() { Success = true, Code = code  };
    public static CommandResult Fail(string errorMessage, string code) => new() { Success = false, ErrorMessage = errorMessage, Code = code };
}
