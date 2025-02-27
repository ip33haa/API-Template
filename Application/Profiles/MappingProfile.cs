using API.Application.Features.UserRole;
using API.Domain.DTOs;
using API.Domain.Entity;
using AutoMapper;

namespace API.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Mapping from User to UserDto (useful for returning user data after registration or login)
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Role, RoleDto>().ReverseMap();
        }
    }
}
