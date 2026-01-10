public record OutputEnvelope<TPayload>
where TPayload : IPayload
{
    public string Type { get; init; }
    public TPayload Payload { get; set; }

    public OutputEnvelope(OutputDomain typeDomain, OutputType type, TPayload payload)
    {
        this.Type = $"{typeDomain.ToString().ToLowerInvariant()}.{type.ToString().ToLowerInvariant()}";
        this.Payload = payload;
    }
}
