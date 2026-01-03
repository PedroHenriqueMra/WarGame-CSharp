public static class Config
{
    public static void Configure(WebApplicationBuilder builder)
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
