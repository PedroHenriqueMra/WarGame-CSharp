using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

ConfigStartup.Startup(builder);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v3", new OpenApiInfo {
        Title = "GTrackAPI",
        Version = "v3"
    });
});

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

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// pages path: ./Src/Client/Pages
var filePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, "Client", "Pages");

var fileProvider = new PhysicalFileProvider(filePath);
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = fileProvider,
    RequestPath = "/pages",
});

app.UseHttpsRedirection();
app.MapControllers();
app.MapControllerRoute("Home", "/");

app.Run();
