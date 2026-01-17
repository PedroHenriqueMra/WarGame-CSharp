using System.Diagnostics;

public class GameLoop
{
    private static NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

    public SnapshotGameBuilder _snapshotBuilder { get; } = new();
    public event Action<GameSnapshot>? OnSnapshot;

    private const int _snapshotRate = 10; // 10 snapshot ticks per
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
        Logger.Trace("GameLoop started!");

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
            _game.Stop();
            Logger.Trace("Game Loop stopped!");
        }
    }

    private async Task RunAsync(CancellationToken ct)
    {
        // GLOBAL VARIABLES
        var stopwatch = Stopwatch.StartNew();
        var lastFrameTime = stopwatch.Elapsed;
        
        int tick = 0;
        while (!ct.IsCancellationRequested)
        {
            // Getting time of the loop beggining
            var currentFrameTime = stopwatch.Elapsed;
            // calculing deltaTime
            var deltaTime = (float)(currentFrameTime - lastFrameTime).TotalSeconds;
            // Atualize lastTime for the next loop
            lastFrameTime = currentFrameTime;

            this._game.Update(deltaTime);
            
            tick++;

            if (ShouldSendSnapshot(tick))
            {
                var gameSnapshot = _snapshotBuilder.Build(_game, tick);
                // send snappshots to room by event
                OnSnapshot?.Invoke(gameSnapshot);
            }
            
            // DEBUG:
            Console.WriteLine($"Tick");

            // Getting constDeltaTime in seconds
            var targetTime = TimeSpan.FromSeconds(_constDeltaTime);
            // Calculating the time that game.Update() took -> Calc: Game start time - Loop start time 
            var elapsedFrameTime = stopwatch.Elapsed - currentFrameTime;
            // Calculating the time to wait to reach the target time -> calc: Time that each frame must take - Time that game.Update() took
            var delay = targetTime - elapsedFrameTime;

            if (delay > TimeSpan.Zero)
            {
                await Task.Delay(delay, ct);
            }
        }
    }

    private bool ShouldSendSnapshot(int tick)
    {
        return tick % _snapshotRate == 0;
    }
}
