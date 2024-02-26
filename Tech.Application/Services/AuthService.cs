using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Tech.Domain.DTO;
using Tech.Domain.DTO.Author;
using Tech.Domain.DTO.User;
using Tech.Domain.Entity;
using Tech.Domain.Enum;
using Tech.Domain.Interfaces.Repostories;
using Tech.Domain.Interfaces.Services;
using Tech.Domain.Result;

namespace Tech.Application.Services;

public class AuthService :IAuthService
{
    private readonly IBaseRepository<User> _repository;
    private readonly IBaseRepository<UserToken> _userTokenRepository;
    private readonly IBaseRepository<Author> _authorRepository;
    private readonly ITokenService _tokenService;
    private readonly ILogger _logger;
    private readonly IMapper _mapper;
    
    public AuthService(IBaseRepository<User> repository, IMapper mapper, ILogger logger, IBaseRepository<UserToken> userTokenRepository, ITokenService tokenService, IBaseRepository<Author> authorRepository)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
        _userTokenRepository = userTokenRepository;
        _tokenService = tokenService;
        _authorRepository = authorRepository;
    }

    public async Task<BaseResult<UserDto>> Register(RegisterUserDto dto)
    {
        if (dto.Password != dto.PasswordConfirm)
        {
            return new BaseResult<UserDto>()
            {
                ErrorMessage = ErrorMessage.PasswordNotEquals,
                ErrorCode = (int)ErrorCode.PasswordNotEquals
            };
        }

        try
        {
            var user = await _repository.GetAll().FirstOrDefaultAsync(x => x.Login == dto.Login);

            if (user != null)
            {
                return new BaseResult<UserDto>()
                {
                    ErrorMessage = ErrorMessage.UserAlreadyExists,
                    ErrorCode = (int)ErrorCode.UserAlreadyExists
                };
            }
            var hasUserPassword = HashPassword(dto.Password);
            user = new User()
            {
                Login = dto.Login,
                Password = hasUserPassword
            };

            await _repository.CreateAsync(user);

            return new BaseResult<UserDto>()
            {
                Data = _mapper.Map<UserDto>(user)
            };
        }
        catch (Exception e)
        {
            _logger.Error(e, e.Message);
            return new BaseResult<UserDto>()
            {
                ErrorMessage = ErrorMessage.InternalServerError,
                ErrorCode = (int)ErrorCode.InternalServerError
            };
        }
    }

    public async Task<BaseResult<TokenDto>> Login(LoginUserDto dto)
    {
        try
        {
            var user = await _repository.GetAll().FirstOrDefaultAsync(x => x.Login == dto.Login);
            if (user == null)
            {
                return new BaseResult<TokenDto>()
                {
                    ErrorMessage = ErrorMessage.InternalServerError,
                    ErrorCode = (int)ErrorCode.InternalServerError
                };
            }

            if (!IsVerifyPassword(user.Password, dto.Password))
            {
                return new BaseResult<TokenDto>()
                {
                    ErrorMessage = ErrorMessage.PasswordIsWrong,
                    ErrorCode = (int)ErrorCode.PasswordIsWrong
                };
            }

            var userToken = await _userTokenRepository.GetAll().FirstOrDefaultAsync(x => x.UserId == user.Id);

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.Login),
                new Claim(ClaimTypes.Role, "User")
            };
            
            var accessToken = _tokenService.GenerateAccessToken(claims);
            
            var refreshToken = _tokenService.GenerateRefreshToken();
            
            if (userToken == null)
            {
                userToken = new UserToken()
                {
                    UserId = user.Id,
                    RefreshToken = refreshToken,
                    RefreshTokenExpireTime = DateTime.UtcNow.AddDays(7)
                };

                await _userTokenRepository.CreateAsync(userToken);
            }
            else
            {
                userToken.RefreshToken = refreshToken;
                userToken.RefreshTokenExpireTime = DateTime.UtcNow.AddDays(7);
                await _userTokenRepository.UpdateAsync(userToken);
            }

            return new BaseResult<TokenDto>()
            {
                Data = new TokenDto()
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken
                }
            };
        }
        catch (Exception e)
        {
            _logger.Error(e, e.Message);
            return new BaseResult<TokenDto>()
            {
                ErrorMessage = ErrorMessage.InternalServerError,
                ErrorCode = (int)ErrorCode.InternalServerError
            };
        }
        
        
    }

    public async Task<BaseResult<CreateAuthorDto>> CreateAuthor(CreateAuthorDto dto)
    {
        try
        {
            
            /*
             * TODO реализовать проверку User'а
             * var user = _repository.GetAll().FirstOrDefaultAsync(x => x.Id == 1);
             */
            var author = new Author()
            {
                UserId = 2,
                Country = dto.Country,
                WorkPlace = dto.WorkPlace
            };

            await _authorRepository.CreateAsync(author);
            
            return new BaseResult<CreateAuthorDto>()
            {
                Data = null
            };
        }
        catch (Exception e)
        {
            _logger.Error(e, e.Message);
            return new BaseResult<CreateAuthorDto>()
            {
                ErrorMessage = ErrorMessage.InternalServerError,
                ErrorCode = (int)ErrorCode.InternalServerError
            };
        }
    }

    private string HashPassword(string pass)
    {
        var encodedPass = Encoding.UTF8.GetBytes(pass);
        var bytes = SHA256.HashData(encodedPass);

        return BitConverter.ToString(bytes).ToLower();
    }

    private bool IsVerifyPassword(string hashUserPass, string userPass)
    {
        var hash = HashPassword(userPass);
        return hashUserPass == hash;
    }
}