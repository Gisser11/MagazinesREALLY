using Tech.Domain.DTO;
using Tech.Domain.DTO.User;
using Tech.Domain.Result;

namespace Tech.Domain.Interfaces.Services;

public interface IAuthService
{ 
    Task<BaseResult<UserDto>> Register(RegisterUserDto dto);
    
    Task<BaseResult<TokenDto>> Login(LoginUserDto dto);

}