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

bot.PlayRandomCommands();

// stop room
Task.Delay(4000).Wait();
await bot.Room.StopAsync();
