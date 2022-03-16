using wakeApi.Dtos;
using wakeApi.Identity;
using AutoMapper;

namespace wakeApi.AutoMapper
{
    public class AutoMapperProfiles : Profile
    {

       public AutoMapperProfiles()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserLoginDto>().ReverseMap();
        }
    }
}
