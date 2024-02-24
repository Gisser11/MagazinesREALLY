using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Tech.Domain.DTO;
using Tech.Domain.DTO.User;
using Tech.Domain.Interfaces.Services;
using Tech.Domain.Result;

namespace Tech.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class AuthController : Controller
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    
    [HttpPost("register")]
    public async Task<ActionResult<BaseResult<UserDto>>> Register([FromBody] RegisterUserDto dto)
    {
        var response = await _authService.Register(dto);
        if (response.IsSuccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }
    
    //
    // [HttpPost("login")]
    // public async Task<ActionResult<BaseResult<TokenDto>>> Login([FromBody] LoginUserDto dto)
    // {
    //     
    // }
}