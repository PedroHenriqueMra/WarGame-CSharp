public record OutputEnvelope<TPayload>
where TPayload : IPayload
{
    public string Type { get; init; }
    public TPayload Payload { get; set; }

    public OutputEnvelope(string type, TPayload payload)
    {
        this.Type = type;
        this.Payload = payload;
    }
}
