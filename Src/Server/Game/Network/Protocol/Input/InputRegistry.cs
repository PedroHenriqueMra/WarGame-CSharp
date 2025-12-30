public static class InputRegistry
{
    private static readonly Dictionary<string, InputDescriptor> _types = new Dictionary<string, InputDescriptor>(StringComparer.OrdinalIgnoreCase);

    public static void Register<TInput>(string type, InputGroup group, bool allowPayload)
        where TInput : IInput
    {
        _types[type] = new InputDescriptor
        {
            Group = group,
            Type = type,
            AllowPayload = allowPayload,
            InputType = typeof(TInput)
        };
    }

    public static InputDescriptor? GetDescriptor(string type)
        => _types.TryGetValue(type, out var d) ? d : null;
}
