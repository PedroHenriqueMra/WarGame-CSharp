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
        return _gameStorage._roomStore.GetRoomInfos();
    }

    [HttpPost]
    [Route("/")]
    public IActionResult PostCreateRoom([FromBody]CreateRoomDto dto)
    {
        var cmd = new CreateRoomCommand(dto.RoomName, dto.UserId);
        var sucess = _systemAdminCommandHandle.Handle(cmd);
        if (sucess)
            return Ok();

        return BadRequest();
    }
}
