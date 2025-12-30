using System.Text.Json;

public sealed class InputEnvelope
{
    public string Type { get; set; } = null!;
    public JsonElement Payload { get; set; }
}
