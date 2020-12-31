using AutoMapper;
using Simple.Application;
using Simple.Domain.Entities;

namespace Simple.Api.Extensions.AutoMapper
{
    public class CustomProfile : Profile
    {
        public CustomProfile()
        {
            #region User

            CreateMap<User, UserDto>().ReverseMap();

            CreateMap<User, UserInfo>().ReverseMap();

            CreateMap<UserDto, UserInfo>().ReverseMap();

            #endregion User
        }
    }
}
