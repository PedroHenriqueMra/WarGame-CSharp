public record InputDescriptor
{
    public InputGroup Group { get; set; }
    public string Type { get; set; }
    public bool AllowPayload { get; set; }
    public Type InputType { get; set; }
}
