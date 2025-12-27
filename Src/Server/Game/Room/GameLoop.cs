using System.Diagnostics;
using System.Linq.Expressions;

public class GameLoop
{
    private CancellationTokenSource _cts;
    private Task _task;
    private Game _game;
    public GameLoop(Game game)
    {
        this._game = game;
    }

    public Task Start()
    {
        if (this._task != null && !this._task.IsCompleted)
            throw new InvalidOperationException("GameLoop already running");

        this._cts = new CancellationTokenSource();

        this._task = this.RunAsync(this._cts.Token);

        return this._task;
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
        // teste:

        while (!ct.IsCancellationRequested)
        {
            var deltaTime = 0.16f; // test
            this._game.Update(deltaTime);

            // test:
            Console.WriteLine("Tick");
            Player? player = this._game.Players.FirstOrDefault();
            if (player != null)
            {
                Console.WriteLine($"Current player pos: X: {player.Position.X} - Y: {player.Position.Y}");
                Console.WriteLine("▅".PadLeft(player.Position.X > 0 ? (int)Math.Ceiling(player.Position.X * 2) : 0));
            }
            Console.WriteLine("<------------------->");
            await Task.Delay(1000, ct);
            Console.Clear();
        }
    }
}
