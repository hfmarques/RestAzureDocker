using Microsoft.AspNetCore.Mvc;
using WebApi.Business;
using WebApi.Data.Vo;

namespace WebApi.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ILoginBusiness loginBusiness;

    public AuthController(ILoginBusiness loginBusiness)
    {
        this.loginBusiness = loginBusiness;
    }

    [HttpPost]
    [Route("signin")]
    public IActionResult Signin(UserVo? user)
    {
        if (user is null)
        {
            return BadRequest();
        }

        var token = loginBusiness.ValidateCredentials(user);
        if (token is null)
        {
            return Unauthorized();
        }

        return Ok(token);
    }

    [HttpPost]
    [Route("refresh")]
    public IActionResult Refresh(TokenVo? user)
    {
        if (user is null)
        {
            return BadRequest();
        }

        var token = loginBusiness.ValidateCredentials(user);
        if (token is null)
        {
            return BadRequest();
        }

        return Ok(token);
    }

    [HttpPut]
    [Route("revoke")]
    public IActionResult Revoke()
    {
        var username = User.Identity?.Name;

        if (username is null) return BadRequest();

        var result = loginBusiness.RevokeToken(username);

        if (!result)
        {
            return BadRequest();
        }

        return NoContent();
    }
}