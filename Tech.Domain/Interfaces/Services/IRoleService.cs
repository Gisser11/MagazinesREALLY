using Tech.Domain.DTO.Role;
using Tech.Domain.Entity;
using Tech.Domain.Result;

namespace Tech.Domain.Interfaces.Services;

public interface IRoleService
{
    Task<BaseResult<Role>> CreateRoleAsync(CreateRoleDto dto);

    Task<BaseResult<Role>> DeleteRoleAsync(long id);

    Task<BaseResult<UserRoleDto>> AddRoleForUserAsync(UserRoleDto dto);


}