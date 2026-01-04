public interface ISystemAdminInput
{
    public InputGroup Group { get; }
    public bool AllowPayload { get; }
    public ISystemAdminCommand? ToCommand(Guid userId);
}
