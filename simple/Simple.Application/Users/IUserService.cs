using System;
using System.Collections.Generic;
using System.Text;

namespace Simple.Application
{
    public interface IUserService
    {
        UserDto Get(int id);

        List<UserDto> GetAll();

        UserInfo GetUser(string account, string password);
    }
}
