public static class ConfigStartup
{
    public static void Startup(WebApplicationBuilder builder)
    {
        // SINGLETONS
        builder.Services.AddSingleton<GameDataStorage>();
        builder.Services.AddSingleton<InputAdmin>();
        builder.Services.AddSingleton<GameAdminCommandHandler>();
        builder.Services.AddSingleton<SystemAdminCommandHandle>();
        builder.Services.AddSingleton<WebSocketHandler>();

        StartupInputDescriptors.SetInputRegisters();
    } 
}
