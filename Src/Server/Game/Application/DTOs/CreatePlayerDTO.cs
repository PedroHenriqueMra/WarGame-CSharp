using System.Runtime.CompilerServices;

public struct CreatePlayerDTO
{
    public string Name { get; set; }

    public CreatePlayerDTO(string name)
    {
        this.Name = name;
    } 
}
