using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{   
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
        return Ok();
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
