public class GameLoop
{
    private CancellationTokenSource _cts;
    private Task _task;
    private Game _game;
    public GameLoop(Game game)
    {
        this._game = game;
    }

    public void Start()
    {
        this._cts = new CancellationTokenSource();

        this._task = this.RunAsync(this._cts.Token);
    }

    public async Task StopAsync()
    {
        this._cts.Cancel();

        try
        {
            await this._task;
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("Game Loop stopped!");
        }
    }

    private async Task RunAsync(CancellationToken ct)
    {
        while (!ct.IsCancellationRequested)
        {
            this._game.Update();
            Console.WriteLine("Game Loop running...");
            Console.WriteLine("<------------------->");
            await Task.Delay(800, ct);
        }
    }
}
