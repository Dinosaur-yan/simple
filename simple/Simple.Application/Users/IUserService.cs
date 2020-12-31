using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Application
{
    public interface IUserService
    {
        Task<UserDto> GetAsync(int id);

        Task<List<UserDto>> GetAllAsync();

        Task<UserInfo> GetUserAsync(string account, string password);
    }
}
