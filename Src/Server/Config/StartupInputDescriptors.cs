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

        // ADMIN    
        InputRegistry.Register<CreateRoomInput>(
            type: "CreateRoom",
            group: InputGroup.Admin,
            allowPayload: true
        );
        InputRegistry.Register<JoinRoomInput>(
            type: "JoinRoom",
            group: InputGroup.Admin,
            allowPayload: true
        );
        InputRegistry.Register<LeaveRoomInput>(
            type: "LeaveRoom",
            group: InputGroup.Admin,
            allowPayload: true
        );
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
    }
}
