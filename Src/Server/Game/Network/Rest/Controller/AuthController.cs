using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly UserAdminHandler _userAdminHandler;
    public AuthController(UserAdminHandler userAdminHandler, IHttpContextAccessor httpContextAccessor)
    {
        _userAdminHandler = userAdminHandler;
        _httpContextAccessor = httpContextAccessor;
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
        Response.Cookies.Append(
            "dev-auth",
            guest.UserId.ToString(),
            new CookieOptions { HttpOnly = true }
        );

        return Ok(new {token = guest.UserId});
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
