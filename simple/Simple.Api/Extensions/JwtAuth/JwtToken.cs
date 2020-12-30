using System;

namespace Simple.Api.Extensions.JwtAuth
{
    public class JwtToken
    {
        /// <summary>
        /// access token
        /// </summary>
        public string access_token { get; set; }

        /// <summary>
        /// refresh token
        /// </summary>
        public string refresh_token { get; set; }

        /// <summary>
        /// expires_in
        /// </summary>
        public long expires_in { get; set; }

        /// <summary>
        /// token type
        /// </summary>
        public string token_type { get; set; }
    }
}
