using System.Net.Sockets;

var builder = WebApplication.CreateBuilder(args);

Config.Configure(builder);

var app = builder.Build();

app.UseWebSockets();

app.MapGet("/ws", async context =>
{
    if (!context.WebSockets.IsWebSocketRequest)
    {
        context.Response.StatusCode = StatusCodes.Status400BadRequest;
        return;
    }

    var handler = context.RequestServices
            .GetRequiredService<WebSocketHandler>();

    using var socket = await context.WebSockets.AcceptWebSocketAsync();
    await handler.HandleWebSocketAsync(socket);
});

app.Run();
