using AutoMapper;
using Tech.Domain.DTO.User;
using Tech.Domain.Entity;

namespace Tech.Application.Mapping;

public class UserMapping : Profile
{
    public UserMapping()
    {
        CreateMap<User, UserDto>().ReverseMap();
    }
}