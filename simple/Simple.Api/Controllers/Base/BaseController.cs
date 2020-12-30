using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Simple.Api.Extensions.JwtAuth;
using System;
using System.Linq;

namespace Simple.Api.Controllers
{
    [Authorize]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected UserInfo UserInfo
        {
            get
            {
                var claims = HttpContext.User.Claims;
                if (claims == null || !claims.Any())
                    throw new Exception("获取用户信息异常！");

                var user = new UserInfo()
                {
                    UserId = Convert.ToInt32(claims.FirstOrDefault(t => t.Type == JwtClaim.Id).Value),
                    UserName = claims.FirstOrDefault(t => t.Type == JwtClaim.Name).Value,
                    RoleId = Convert.ToInt32(claims.FirstOrDefault(t => t.Type == JwtClaim.RoleId).Value)
                };
                return user;
            }
        }
    }
}
