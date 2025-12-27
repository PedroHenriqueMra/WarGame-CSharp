using System.Diagnostics;
using System.Linq.Expressions;

public class GameLoop
{
    private const float _tickRate = 60f; // 60 ticks per second
    private const float _constDeltaTime = 1f / _tickRate; // ≃ 0.016f

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
        // GLOBAL VARIABLES
        var stopwatch = Stopwatch.StartNew();
        var lastFrameTime = stopwatch.Elapsed;
        
        while (!ct.IsCancellationRequested)
        {
            // Getting time of the loop beggining
            var currentFrameTime = stopwatch.Elapsed;
            // calculing deltaTime
            var deltaTime = (float)(currentFrameTime - lastFrameTime).TotalSeconds;
            // Atualize lastTime for the next loop
            lastFrameTime = currentFrameTime;

            this._game.Update(deltaTime);

            // Getting constDeltaTime in seconds
            var targetTime = TimeSpan.FromSeconds(_constDeltaTime);
            // Calculating the time that game.Update() took -> Calc: Game start time - Loop start time 
            var elapsedFrameTime = stopwatch.Elapsed - currentFrameTime;
            // Calculating the time to wait to reach the target time -> calc: Time that each frame must take - Time that game.Update() took
            var delay = targetTime - elapsedFrameTime;

            if (delay > TimeSpan.Zero)
            {
                // test:
                Console.WriteLine($"Tick. Delay: {delay}");
                if (_game.Players.Count > 0)
                {
                    Console.WriteLine($"{_game.Players[0].Name} Position: {_game.Players[0].Position.X}");
                    Console.WriteLine(@"~=ō͡≡o˞̶".PadLeft((int)_game.Players[0].Position.X));
                }
                
                await Task.Delay(delay, ct);
            }
        }
    }
}
