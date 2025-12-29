//var builder = WebApplication.CreateBuilder(args);
//var app = builder.Build();
//
//app.MapGet("/", () => "Hello World!");
//
//app.Run();


// objects
//RoomAdmin roomAdmin = new RoomAdmin();
Bot bot = new Bot();

// bot operation
bot.CreateRoom();
bot.startGame();

//Task.Delay(1000).Wait();
bot.CreatePlayer();
//Task.Delay(1000).Wait();
bot.JoinRoom();
//Task.Delay(1000).Wait();

try
{
    bot.PlayBotCommandsAsync();
} 
catch (InvalidOperationException ex)
{
    Console.WriteLine($"Bot stopped. {ex.Message}");
}

// stop room
Task.Delay(7000).Wait();
await bot.Room.StopAsync();
