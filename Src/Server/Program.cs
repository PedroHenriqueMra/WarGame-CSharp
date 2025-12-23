//var builder = WebApplication.CreateBuilder(args);
//var app = builder.Build();
//
//app.MapGet("/", () => "Hello World!");
//
//app.Run();

// Create run
RoomAdmin roomAdmin = new RoomAdmin();
Room room1 = roomAdmin.TryCreateRoom(new CreateRoomDTO(1, "Room 1"));
Room room2 = roomAdmin.TryCreateRoom(new CreateRoomDTO(1, "Room 2"));
Room room3 = roomAdmin.TryCreateRoom(new CreateRoomDTO(1, "Room 3"));

// run game
// room 1:
room1.Start();

// room 2:
room2.Start();

// room 3:
room3.Start();

await Task.Delay(5000);
await room1.StopAsync();
await room2.StopAsync();
await room3.StopAsync();
