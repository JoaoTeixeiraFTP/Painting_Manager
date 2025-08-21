using Microsoft.AspNetCore.Mvc;
using PaintingManager.Api.Services;
using PaintingManager.Api.Dtos;

namespace PaintingManager.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginDto dto)
    {
        var user = _authService.ValidateUser(dto);
        if (user == null)
            return Unauthorized(new { message = "Credenciais inválidas" });

        var token = _authService.GenerateJwtToken(user);
        return Ok(new { token, role = user.Role });
    }
}
