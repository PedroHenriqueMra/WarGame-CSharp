public interface IGameplayCommand : IGameCommand
{
     public Session Session { get; set; }
     public void Execute(Game game);
}
