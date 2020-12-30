using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Simple.Api.Extensions.JwtAuth;
using Simple.Application;
using System;
using System.Linq;

namespace Simple.Api.Controllers
{
    /// <summary>
    /// Token
    /// </summary>
    [Route("api/token")]
    public class TokenController : BaseController
    {
        private readonly IJwtFactory jwtFactory;
        private readonly IMemoryCache memoryCache;
        private readonly IUserService userService;

        public TokenController(
            IJwtFactory jwtFactory,
            IMemoryCache memoryCache,
            IUserService userService
            )
        {
            this.jwtFactory = jwtFactory;
            this.memoryCache = memoryCache;
            this.userService = userService;
        }

        /// <summary>
        /// 获取Token
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public ApiResult<JwtToken> GetToken([FromBody] UserDto dto)
        {
            var user = userService.GetUser(dto.Account, dto.Password);
            if (user == null) return new ApiResult<JwtToken>(ResultType.NotFound);

            var refreshToken = Guid.NewGuid().ToString();

            memoryCache.Set(refreshToken, user.UserName, TimeSpan.FromMinutes(11));

            var token = jwtFactory.GenerateEncodedToken(refreshToken, user);

            return new ApiResult<JwtToken>(ResultType.OK, token);
        }

        /// <summary>
        /// 刷新Token
        /// </summary>
        /// <returns></returns>
        [HttpPost("refresh")]
        public ApiResult<JwtToken> RefreshToken([FromQuery] string refreshToken)
        {
            if (!memoryCache.TryGetValue(refreshToken, out string userName))
                return new ApiResult<JwtToken>(ResultType.BadRequest, "登录失败: refreshtoken 无效.");

            if (!UserIdentity.UserName.Equals(userName))
                return new ApiResult<JwtToken>(ResultType.Forbidden, "登录失败: 用户名 无效.");

            string newRefreshToken = Guid.NewGuid().ToString();
            memoryCache.Remove(refreshToken);
            memoryCache.Set(newRefreshToken, UserIdentity.UserName, TimeSpan.FromMinutes(11));

            var token = jwtFactory.GenerateEncodedToken(newRefreshToken, UserIdentity);

            return new ApiResult<JwtToken>(ResultType.OK, token);
        }
    }
}
