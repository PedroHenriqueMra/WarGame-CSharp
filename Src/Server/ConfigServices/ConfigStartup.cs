using System.Net.WebSockets;

public static class ConfigStartup
{
    public static void Startup(WebApplicationBuilder builder)
    {
        // SINGLETONS
        builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        builder.Services.AddSingleton<IUserStore, InMemoryUserStore>();
        builder.Services.AddSingleton<IRoomStore, InMemoryRoomStore>();
        builder.Services.AddSingleton<GameDataStorage>();

        builder.Services.AddSingleton<InputAdmin>();
        builder.Services.AddSingleton<IRoomBindingValidator, RoomBindingValidator>();
        builder.Services.AddSingleton<IUserIdentifierProvider, UserIdentifierProvider>();

        builder.Services.AddSingleton<GameAdminCommandHandler>();
        builder.Services.AddSingleton<SystemAdminCommandHandle>();
        builder.Services.AddSingleton<UserAdminHandler>();
        
        builder.Services.AddSingleton<InputAdmin>();
        builder.Services.AddSingleton<SendOutput>();
        builder.Services.AddSingleton<IConnectSession<WebSocket>, ConnectSessionWS>();
        builder.Services.AddSingleton<HandlerHandShake>();
        builder.Services.AddSingleton<WebSocketHandler>();

        //CONFIG REGISTRY
        StartupInputDescriptors.SetInputRegisters();
    } 
}
