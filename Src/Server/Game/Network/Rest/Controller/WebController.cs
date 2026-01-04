using Microsoft.AspNetCore.Mvc;

public class WebController : ControllerBase
{
    [HttpGet]
    [Route("room")]
    public ContentResult GetRoom()
    {
        var filePath = GlobalConstants.PATH_PAGES + "/Rooms.html";
        var fileContents = System.IO.File.ReadAllText(filePath);

        return Content(fileContents, "text/html");
    }
}
