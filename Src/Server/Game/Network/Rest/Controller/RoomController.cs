using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/room")]
public class RoomController : ControllerBase
{
    private readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

    private readonly GameDataStorage _gameStorage;
    private readonly SystemAdminCommandHandle _systemAdminCommandHandle;
    public RoomController(GameDataStorage gameStorage, SystemAdminCommandHandle systemAdminCommandHandle)
    {
        _gameStorage = gameStorage;
        _systemAdminCommandHandle = systemAdminCommandHandle;
    }

    [HttpGet]
    [Route("list")]
    public IReadOnlyList<RoomInfoDto> GetRooms()
    {
        return _gameStorage.GetRoomInfos();
    }

    [HttpPost]
    [Route("")]
    public IActionResult PostCreateRoom([FromBody]CreateRoomDto dto)
    {
        var cmd = new CreateRoomCommand(dto.RoomName, dto.UserId);
        var result = _systemAdminCommandHandle.Handle(cmd) ?? SystemAdminResult.Fail("Fail by null value", "CREATE_ROOM_FAIL");

        Logger.Info(result.Status ? $"Create room success: Name: {dto.RoomName}" : $"Create room fail!: RoomName: {dto.RoomName}, error message: {result.Message}");
        
        return Result(result);
    }

    [HttpPut]
    [Route("join/{roomId}")]
    public IActionResult PutJoinRoom(Guid roomId, [FromBody]JoinRoomDto dto)
    {
        var cmd = new JoinRoomCommand(roomId, dto.UserId);
        var result = _systemAdminCommandHandle.Handle(cmd) ?? SystemAdminResult.Fail("Fail by null value", "JOIN_ROOM_FAIL");
        
        Logger.Info(result.Status ? $"Join room success: roomId: {roomId}" : $"Join room fail!: romId: {roomId}, error message: {result.Message}");

        return Result(result);
    }

    [HttpPut]
    [Route("leave/{roomId}")]
    public IActionResult PutLeaveRoom(Guid roomId, [FromBody]LeaveRoomDto dto)
    {
        var cmd = new LeaveRoomCommand(roomId, dto.UserId);
        var result = _systemAdminCommandHandle.Handle(cmd) ?? SystemAdminResult.Fail("Fail by null value", "LEAVE_ROOM_FAIL");
        
        Logger.Info(result.Status ? $"Leave room success: roomId: {roomId}" : $"Leave room fail!: romId: {roomId}, error message: {result.Message}");

        return Result(result);
    }

    private IActionResult Result(SystemAdminResult result)
    {
        var response = new ApiResponse
        (
            result.Status,
            result.Message,
            result.Code
        );
        
        return result.Status ? Ok(response) : BadRequest(response);
    }
}
