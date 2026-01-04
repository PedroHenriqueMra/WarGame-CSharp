using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api")]
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
    [Route("room/list")]
    public IReadOnlyList<RoomListDto> GetRooms()
    {
        return _gameStorage.GetRoomInfos();
    }

    [HttpPost]
    [Route("room")]
    public IActionResult PostCreateRoom([FromBody]CreateRoomDto dto)
    {
        var cmd = new CreateRoomCommand(dto.RoomName, dto.UserId);
        var sucess = _systemAdminCommandHandle.Handle(cmd);
        if (sucess)
            return Ok();

        return BadRequest();
    }
}
