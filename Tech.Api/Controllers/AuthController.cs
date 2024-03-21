using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Tech.Domain.DTO;
using Tech.Domain.DTO.User;
using Tech.Domain.Interfaces.Services;

namespace Tech.Api.Controllers;


[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost]
    public async Task<ActionResult<UserDto>> Register(RegisterUserDto dto)
    {
        var response = await _authService.Register(dto);
        if (response.IsSuccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }
    
    [HttpPost("login")]
    public async Task<ActionResult<TokenDto>>Login(LoginUserDto dto)
    {
        var response = await _authService.Login(dto);
        if (response.IsSuccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }
}