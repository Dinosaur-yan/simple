using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Simple.Api.Extensions.JwtAuth
{
    public class JwtFactory : IJwtFactory
    {
        private readonly JwtOptions _jwtOptions;

        public JwtFactory(IOptions<JwtOptions> options)
        {
            _jwtOptions = options?.Value ?? throw new ArgumentNullException(nameof(JwtOptions));
        }

        public JwtToken GenerateEncodedToken(string refreshToken, UserInfo user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtClaim.Id, user.UserId.ToString()),
                new Claim(JwtClaim.Name, user.UserName),
                new Claim(JwtClaim.RoleId, user.RoleId.ToString()),
                new Claim(JwtClaim.Email,"")
            };

            var jwt = new JwtSecurityToken
            (
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                notBefore: _jwtOptions.NotBefore,
                expires: _jwtOptions.Expiration,
                signingCredentials: _jwtOptions.SigningCredentials
            );

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var token = new JwtToken
            {
                access_token = encodedJwt,
                refresh_token = refreshToken,
                expires_in = (int)_jwtOptions.ValidFor.TotalSeconds,
                token_type = "Bearer"
            };

            return token;
        }
    }
}
