using AutoMapper;
using Simple.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Application
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public UserService(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<UserDto> GetAsync(int id)
        {
            return _mapper.Map<UserDto>(await _userRepository.GetAsync(id));
        }

        public async Task<List<UserDto>> GetAllAsync()
        {
            return _mapper.Map<List<UserDto>>(await _userRepository.GetAllAsync());
        }

        public async Task<UserInfo> GetUserAsync(string account, string password)
        {
            var user = await _userRepository.FirstOrDefaultAsync(t => t.Account == account && t.Password == password);
            return _mapper.Map<UserInfo>(user);
        }
    }
}
