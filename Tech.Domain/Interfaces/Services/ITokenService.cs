using System.Security.Claims;
using Tech.Domain.DTO;
using Tech.Domain.Result;

namespace Tech.Domain.Interfaces.Services;

public interface ITokenService
{
    string GenerateAccessToken(IEnumerable<Claim> claims);
    
    string GenerateRefreshToken();

    Task<BaseResult<TokenDto>> RefreshToken(TokenDto dto);

    ClaimsPrincipal GetPrincipalFromExpiredToken(string accessToken);


}