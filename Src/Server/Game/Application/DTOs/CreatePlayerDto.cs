using System.Runtime.CompilerServices;

public record CreatePlayerDto
{
    public string Name { get; set; }

    public CreatePlayerDto(string name)
    {
        this.Name = name;
    } 
}
