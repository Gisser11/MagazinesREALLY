using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Tech.Application.Utilities;
using Tech.Domain.DTO;
using Tech.Domain.DTO.Author;
using Tech.Domain.DTO.User;
using Tech.Domain.Entity;
using Tech.Domain.Enum;
using Tech.Domain.Interfaces.Databases;
using Tech.Domain.Interfaces.Repositories;
using Tech.Domain.Interfaces.Services;
using Tech.Domain.Result;

namespace Tech.Application.Services;

public class AuthService :IAuthService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBaseRepository<User> _repository;
    private readonly IBaseRepository<UserToken> _userTokenRepository;
    private readonly IBaseRepository<Author> _authorRepository;
    private readonly IBaseRepository<Role> _roleRepository;
    private readonly IBaseRepository<UserRole> _userRoleRepository;
    private readonly ITokenService _tokenService;
    private readonly IMapper _mapper;
    private readonly PasswordUtility passwordUtility;
    public AuthService(IBaseRepository<User> repository, IMapper mapper,
        IBaseRepository<UserToken> userTokenRepository, ITokenService tokenService, 
        IBaseRepository<Author> authorRepository, IBaseRepository<Role> roleRepository, IBaseRepository<UserRole> userRoleRepository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _mapper = mapper;
        _userTokenRepository = userTokenRepository;
        _tokenService = tokenService;
        _authorRepository = authorRepository;
        _roleRepository = roleRepository;
        _userRoleRepository = userRoleRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<BaseResult<UserDto>> Register(RegisterUserDto dto)
    {
        if (dto.Password != dto.PasswordConfirm)
        {
            var failedResult = ReturnNewResposneUtility<UserDto>
                .CreateFailedResponse(ErrorMessage.PasswordNotEquals, (int)ErrorCode.PasswordNotEquals);
            
            return failedResult;
        }

        var user = await _repository.GetAll().FirstOrDefaultAsync(x => x.Login == dto.Login);

        if (user != null)
        {
            var failedResult = ReturnNewResposneUtility<UserDto>
                .CreateFailedResponse(ErrorMessage.UserAlreadyExists, (int)ErrorCode.UserAlreadyExists);
            
            return failedResult;
        }
        
        var hashUserPassword = HashPassword(dto.Password);

        using (var transaction = await _unitOfWork.BeginTransactionAsync())
        {
            try
            {
                user = new User()
                {
                    Login = dto.Login,
                    Password = hashUserPassword
                };

                await _unitOfWork.Users.CreateAsync(user);
                
                //TODO Обработать нормально роли
                var role = _roleRepository.GetAll().FirstOrDefaultAsync(x => x.Name == "User");
                if (role == null)
                {
                    var failedResult = ReturnNewResposneUtility<UserDto>
                        .CreateFailedResponse(ErrorMessage.RoleAlreadyExists, (int)ErrorCode.RoleAlreadyExists);
            
                    return failedResult;
                }

                UserRole userRole = new UserRole()
                {
                    UserId = user.Id,
                    RoleId = role.Id
                };

                await _unitOfWork.UserRoles.CreateAsync(userRole);

                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
            }
        }
        
        return new BaseResult<UserDto>()
        {
            Data = _mapper.Map<UserDto>(user)
        };
    }

    public async Task<BaseResult<TokenDto>> Login(LoginUserDto dto)
    {
        var user = await _repository.GetAll()
                .Include(x => x.Roles)
                .FirstOrDefaultAsync(x => x.Login == dto.Login);
        
        if (user == null)
        {
            var failedReturn =  ReturnNewResposneUtility<TokenDto>
                .CreateFailedResponse(ErrorMessage.UserNotFound, (int)ErrorCode.UserNotFound);

            return failedReturn;
        }
        
        if (passwordUtility.IsVerifyPassword(user.Password, dto.Password))
        {
            var failedReturn =  ReturnNewResposneUtility<TokenDto>
                .CreateFailedResponse(ErrorMessage.PasswordNotEquals, (int)ErrorCode.PasswordNotEquals);

            return failedReturn;
        }

        var userToken = await _userTokenRepository.GetAll().FirstOrDefaultAsync(x => x.UserId == user.Id);
        var userRoles = user.Roles;
        var claims = userRoles.Select(x => new Claim(ClaimTypes.Role, x.Name)).ToList();
        
        claims.Add(new Claim(ClaimTypes.Name, user.Login));
        
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
            
            _userTokenRepository.Update(userToken);
            await _userTokenRepository.SaveChangesAsync(); 
        }

        var data = new TokenDto()
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
        
        var successReturn = ReturnNewResposneUtility<TokenDto>
            .CreateSuccessResponse(data);

        return successReturn;
    }

    public async Task<BaseResult<CreateAuthorDto>> CreateAuthor(CreateAuthorDto dto)
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

    private string HashPassword(string pass)
    {
        var encodedPass = Encoding.UTF8.GetBytes(pass);
        var bytes = SHA256.HashData(encodedPass);
        return BitConverter.ToString(bytes).ToLower();
    }
}