using AutoMapper;
using Simple.Application;

namespace Simple.Api.Extensions.AutoMapper
{
    public class CustomProfile : Profile
    {
        public CustomProfile()
        {
            CreateMap<UserInfo, UserDto>().ReverseMap();
        }
    }
}
