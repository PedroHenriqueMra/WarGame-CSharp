public record OutputEnvelope
{
    public string Type { get; init; }
    public object Payload { get; set; }

    public OutputEnvelope(string type, object payload)
    {
        this.Type = type;
        this.Payload = payload;
    }
}
