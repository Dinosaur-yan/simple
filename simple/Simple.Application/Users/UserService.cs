using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Simple.Application
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;

        public UserService(IMapper mapper)
        {
            _mapper = mapper;

            MockUsers = new List<UserDto>
            {
                new UserDto { Id = 1, Account = "Admin", Password = "1111", UserName = "系统管理员" },
                new UserDto { Id = 2, Account = "DevUser", Password = "1111", UserName = "开发人员" },
                new UserDto { Id = 2, Account = "TestUser", Password = "1111", UserName = "测试人员" },
            };
        }

        private readonly List<UserDto> MockUsers = null;

        public UserDto Get(int id)
        {
            return MockUsers.Find(t => t.Id == id);
        }

        public List<UserDto> GetAll()
        {
            return MockUsers;
        }

        public UserInfo GetUser(string account, string password)
        {
            var user = MockUsers.Find(t => t.Account == account && t.Password == password);
            return _mapper.Map<UserInfo>(user);
        }
    }
}
