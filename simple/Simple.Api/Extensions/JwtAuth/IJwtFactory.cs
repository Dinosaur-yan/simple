using System;

namespace Simple.Api.Extensions.JwtAuth
{
    public interface IJwtFactory
    {
        JwtToken GenerateEncodedToken(string refreshToken, UserInfo user);
    }
}
