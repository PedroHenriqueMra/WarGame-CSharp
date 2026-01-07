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
        var sucess = _systemAdminCommandHandle.Handle(cmd);
        if (sucess)
            return Ok();

        return BadRequest();
    }

    [HttpPut]
    [Route("join/{roomId}")]
    public IActionResult PutJoinRoom(int roomId, [FromBody]JoinRoomDto dto)
    {
        var cmd = new JoinRoomCommand(roomId, dto.UserId);
        if (_systemAdminCommandHandle.Handle(cmd))
            return Ok();

        return BadRequest();
    }

    [HttpPut]
    [Route("leave/{roomId}")]
    public IActionResult PutLeaveRoom(int roomId, [FromBody]LeaveRoomDto dto)
    {
        var cmd = new LeaveRoomCommand(roomId, dto.UserId);
        if (_systemAdminCommandHandle.Handle(cmd))
            return Ok();

        return BadRequest();
    }
}

