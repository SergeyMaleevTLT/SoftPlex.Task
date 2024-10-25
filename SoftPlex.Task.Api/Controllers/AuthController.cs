using Microsoft.AspNetCore.Mvc;
using SoftPlex.Task.Core.Domain.Dto;
using SoftPlex.Task.Core.Infrastructure.Auth;

namespace SoftPlex.Task.API.Controllers;

[Route("api/[controller]")]
[Produces("application/json")]
public class AuthController : Controller
{
    private readonly IAuthService _authService;
    
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    
    [Route("login")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> Login([FromBody] UserLoginDto request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var user = await _authService.GetUserAuthServiceAsync(request);
        if (user == null) return NotFound();

        var checkResult = await _authService.SecurityPasswordCheck(user, request.Password);

        return checkResult
            ? Ok(await _authService.AuthenticateAsync(user))
            : Forbid();
    }
    
    [Route("Registration")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> Registration([FromBody] UserLoginDto request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var userAny = await _authService.AnyUserByLoginAuthServiceAsync(request.Login);
        if (userAny) return BadRequest();

        return Ok(await _authService.AddUserAuthServiceAsync(request));

    }
}