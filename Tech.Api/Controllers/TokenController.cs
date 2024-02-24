using Microsoft.AspNetCore.Mvc;
using Tech.Domain.DTO;
using Tech.Domain.Interfaces.Services;
using Tech.Domain.Result;

namespace Tech.Api.Controllers;

/// <summary>
/// 
/// </summary>

[ApiController]
public class TokenController : Controller
{
    private readonly ITokenService _tokenService;

    public TokenController(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }

    [HttpPost]
    public async Task<ActionResult<BaseResult<TokenDto>>> RefreshToken([FromBody] TokenDto tokenDto)
    {
        var response = await _tokenService.RefreshToken(tokenDto);
        
        if (response.IsSuccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }
}