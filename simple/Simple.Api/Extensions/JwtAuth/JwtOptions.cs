using Microsoft.IdentityModel.Tokens;
using System;
using System.Threading.Tasks;

namespace Simple.Api.Extensions.JwtAuth
{
    public class JwtOptions
    {
        /// <summary>
        /// Issuer jwt签发者
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// Subject jwt所面向的用户
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Audience 接收jwt的一方
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// Not Before 定义在什么时间之前,该jwt是不可以用的
        /// </summary>
        public DateTime NotBefore => DateTime.Now;

        /// <summary>
        /// Expiration Time jwt的过期时间,必须大于签发时间
        /// </summary>
        public DateTime Expiration => IssuedAt.Add(ValidFor);

        /// <summary>
        /// Issued At jwt的签发时间
        /// </summary>
        public DateTime IssuedAt => DateTime.Now;

        /// <summary>
        /// Set the timespan the token will be valid for (default is 10 min)
        /// </summary>
        public TimeSpan ValidFor { get; set; } = TimeSpan.FromMinutes(10);

        /// <summary>
        /// "jti" (JWT ID) Claim (default ID is a GUID)
        /// </summary>
        public Func<Task<string>> JtiGenerator => () => Task.FromResult(Guid.NewGuid().ToString());

        /// <summary>
        /// The signing key to use when generating tokens.
        /// </summary>
        public SigningCredentials SigningCredentials { get; set; }
    }
}
