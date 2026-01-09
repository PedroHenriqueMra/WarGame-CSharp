using Microsoft.AspNetCore.Mvc;

[Controller]
public class WebController : ControllerBase
{
    [HttpGet]
    [Route("/")]
    public ContentResult GetIndex()
    {
        var filePath = GlobalConstants.PATH_PAGES + "/index.html";
        var fileContents = System.IO.File.ReadAllText(filePath);

        return Content(fileContents, "text/html");
    }

    [HttpGet]
    [Route("signin")]
    public ContentResult GetSignIn()
    {
        var filePath = GlobalConstants.PATH_PAGES + "/signin.html";
        var fileContents = System.IO.File.ReadAllText(filePath);

        return Content(fileContents, "text/html");
    }
    [HttpGet]
    [Route("signup")]
    public ContentResult GetSignUp()
    {
        var filePath = GlobalConstants.PATH_PAGES + "/signup.html";
        var fileContents = System.IO.File.ReadAllText(filePath);

        return Content(fileContents, "text/html");
    }

    [HttpGet]
    [Route("room")]
    public ContentResult GetRoom()
    {
        var filePath = GlobalConstants.PATH_PAGES + "/room.html";
        var fileContents = System.IO.File.ReadAllText(filePath);

        return Content(fileContents, "text/html");
    }
}
