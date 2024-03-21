using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Tech.Domain.DTO;
using Tech.Domain.DTO.User;
using Tech.Domain.Entity;
using Tech.Domain.Interfaces.Services;
using Tech.Domain.Result;

namespace Tech.Application.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly IConfiguration _configuration;

    public AuthService(UserManager<User> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    public async Task<BaseResult<UserDto>> Register(RegisterUserDto dto)
    {
        //TODO валидация пароля. (Pass and PassConfirm)
        var identityUser = new User
        {
            Email = dto.Email,
            UserName = dto.Login
        };

        await _userManager.CreateAsync(identityUser, dto.Password);

        return new BaseResult<UserDto>
        {
            Data = null
        };
    }

    public async Task<BaseResult<TokenDto>> Login(LoginUserDto dto)
    {
        var identityUser = await _userManager.FindByEmailAsync(dto.Login);
        
        var result = await _userManager.CheckPasswordAsync(identityUser, dto.Password);
        
        if (result is false)
        {
            throw new Exception();
        }
        
        TokenDto tokenDto = new TokenDto
        {  
            AccessToken = GenerateAccessToken(identityUser.Email)
        };
        return new BaseResult<TokenDto>
        {
            Data = tokenDto
        };
        
    }

    private string GenerateAccessToken(string email)
    {
        IEnumerable<Claim> claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Email, email)
        };
        SecurityKey securityKey =
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:JwtKey").Value));
        
        SigningCredentials _ = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);
        
        SecurityToken securityToken = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddMinutes(2),
            issuer: _configuration.GetSection("Jwt:Issuer").Value,
            audience:_configuration.GetSection("Jwt:Audience").Value,
            signingCredentials:_
            );
        
        string tokenString = new JwtSecurityTokenHandler().WriteToken(securityToken);
        return tokenString;
    }
    
}