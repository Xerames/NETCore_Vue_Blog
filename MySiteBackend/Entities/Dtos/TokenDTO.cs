using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
    public class TokenDTO
    {
        public string AccessToken { get; set; }

        public DateTime AccessTokenExpiration { get; set; }

        public string RefreshToken { get; set; }

        public DateTime RefreshTokenExpiration { get; set; }
    }
}
