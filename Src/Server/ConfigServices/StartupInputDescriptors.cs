using System.Text.RegularExpressions;

public static class StartupInputDescriptors
{
    public static void SetInputRegisters()
    {
        // GAMEPLAY
        InputRegistry.Register<MoveInput>(
            type: "Move",
            group: InputGroup.Gameplay,
            allowPayload: true
        );
        InputRegistry.Register<JumpInput>(
            type: "Jump",
            group: InputGroup.Gameplay,
            allowPayload: false
        );

        // GAME ADMIN 
        InputRegistry.Register<StartGameInput>(
            type: "StartGame",
            group: InputGroup.Admin,
            allowPayload: true
        );
        InputRegistry.Register<StopGameInput>(
            type: "StopGame",
            group: InputGroup.Admin,
            allowPayload: true
        );

        // SYSTEM ADMIN
        InputRegistry.Register<CreateRoomInput>(
           type: "CreateRoom",
           group: InputGroup.System,
           allowPayload: true
       );
        InputRegistry.Register<JoinRoomInput>(
            type: "JoinRoom",
            group: InputGroup.System,
            allowPayload: true
        );
        InputRegistry.Register<LeaveRoomInput>(
            type: "LeaveRoom",
            group: InputGroup.System,
            allowPayload: true
        );
    }
}
