using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly UserAdminHandler _userAdminHandler;
    public AuthController(UserAdminHandler userAdminHandler)
    {
        _userAdminHandler = userAdminHandler;
    }

    [HttpPost]
    [Route("signin")]
    public IActionResult PostSignIn([FromBody]SignInDto data)
    {
        return Ok();
    }
    [HttpPost]
    [Route("guest")]
    public IActionResult PostCreateGuest()
    {
        User? guest = _userAdminHandler.CreateGuest();

        return Ok(new JsonResult(guest.UserId));
    }

    [HttpPost]
    [Route("signup")]
    public IActionResult PostSignUp([FromBody]SignUpDto data)
    {
        return Ok();
    }

    [HttpPost]
    [Route("signout")]
    public IActionResult PostSignOut()
    {
        return Ok();
    }
}
