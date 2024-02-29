using Microsoft.EntityFrameworkCore;
using Tech.Domain.DTO.Role;
using Tech.Domain.Entity;
using Tech.Domain.Enum;
using Tech.Domain.Interfaces.Repositories;
using Tech.Domain.Interfaces.Services;
using Tech.Domain.Result;

namespace Tech.Application.Services;

public class RoleService : IRoleService
{
    private readonly IBaseRepository<User> _userRepository;
    private readonly IBaseRepository<Role> _roleRepostiry;
    private readonly IBaseRepository<UserRole> _userRoleRepostiory;

    public RoleService(IBaseRepository<User> userRepository, 
        IBaseRepository<Role> roleRepostiry, IBaseRepository<UserRole> userRoleRepostiory)
    {
        _userRepository = userRepository;
        _roleRepostiry = roleRepostiry;
        _userRoleRepostiory = userRoleRepostiory;
    }

    public async Task<BaseResult<Role>> CreateRoleAsync(CreateRoleDto dto)
    {
        var role = await _roleRepostiry.GetAll().FirstOrDefaultAsync(x => x.Name == dto.Name);

        if (role != null)
        {
            return new BaseResult<Role>()
            {
                ErrorMessage = ErrorMessage.RoleAlreadyExists,
                ErrorCode = (int)ErrorCode.RoleAlreadyExists
            };
        }

        role = new Role()
        {
            Name = dto.Name
        };

        await _roleRepostiry.CreateAsync(role);

        return new BaseResult<Role>()
        {
            Data = role
        };

    }

    public async Task<BaseResult<Role>> DeleteRoleAsync(long id)
    {
        var role = await _roleRepostiry.GetAll().FirstOrDefaultAsync(x => x.Id == id);
        
        // TODO сделать ресурсы под этот сервис
        if (role == null)
        {
            return new BaseResult<Role>()
            {
                ErrorMessage = ErrorMessage.RoleAlreadyExists,
                ErrorCode = (int)ErrorCode.RoleAlreadyExists
            };
        }

        _roleRepostiry.Remove(role);
        await _roleRepostiry.SaveChangesAsync();

        return new BaseResult<Role>()
        {
            Data = role
        };
    }

    public async Task<BaseResult<UserRoleDto>> AddRoleForUserAsync(UserRoleDto dto)
    {
        var user = await _userRepository.GetAll()
            .Include(x => x.Roles)
            .FirstOrDefaultAsync(x => x.Login == dto.Login);

        if (user == null)
        {
            return new BaseResult<UserRoleDto>()
            {
                ErrorMessage = ErrorMessage.UserNotFound,
                ErrorCode = (int)ErrorCode.UserNotFound
            };
        }

        var roles = user.Roles.Select(x => x.Name).ToArray();

        var validateRole = roles.All(x => x != dto.RoleName);

        if (validateRole)
        {
            var role = await _roleRepostiry.GetAll()
                .FirstOrDefaultAsync(x => x.Name == dto.RoleName);
            if (role == null)
            {
                return new BaseResult<UserRoleDto>()
                {
                    //TODO Добавить культуру
                    ErrorMessage = "ДОБАВИТЬ КУЛЬТУРУ СЮДА",
                    ErrorCode = (int)ErrorCode.RoleAlreadyExists
                };
            }

            UserRole userRole = new UserRole()
            {
                RoleId = role.Id,
                UserId = user.Id
            };

            await _userRoleRepostiory.CreateAsync(userRole);

            return new BaseResult<UserRoleDto>()
            {
                Data = new UserRoleDto()
                {
                    Login = user.Login,
                    RoleName = role.Name
                }
            };
        }
        
        return new BaseResult<UserRoleDto>()
        {
            ErrorCode = 34,
            ErrorMessage = "UserAlreadyHasRole"
        };
    }
}