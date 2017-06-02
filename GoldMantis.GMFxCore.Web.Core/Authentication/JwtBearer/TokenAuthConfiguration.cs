using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace GoldMantis.GMFxCore.Web.Core.Authentication.JwtBearer
{
    public class TokenAuthConfiguration
    {
        public string Path { get; set; } = "/token";

        public SymmetricSecurityKey SecurityKey { get; set; }

        public string Issuer { get; set; }

        public string Audience { get; set; }

        public SigningCredentials SigningCredentials { get; set; }

        public TimeSpan Expiration { get; set; }
    }
}
