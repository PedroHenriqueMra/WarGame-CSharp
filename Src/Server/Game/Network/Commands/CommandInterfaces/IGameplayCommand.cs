public interface IGameplayCommand : ICommand
{
     public Session Session { get; set; }
     public int InputTick { get; set; }
     public void Execute(Game game);
}
