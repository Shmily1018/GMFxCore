using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoldMantis.GMFxCore.Extensions;
using GoldMantis.GMFxCore.Web.Core.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;

namespace GoldMantis.GMFxCore.Web.StartUp
{
    public static class AuthorizationConfigurer
    {
        public static void Configure(IApplicationBuilder app, IConfiguration configuration)
        {
            //??? use identity
            //app.UseIdentity();

            if (bool.Parse(configuration["Authentication:OpenId:IsEnabled"]))
            {
                app.UseOpenIdConnectAuthentication(CreateOpenIdConnectAuthOptions(configuration));
            }

            if (bool.Parse(configuration["Authentication:JwtBearer:IsEnabled"]))
            {
                app.UseJwtBearerAuthentication(CreateJwtBearerAuthenticationOptions(app));
            }

            //if (bool.Parse(configuration["Authentication:Cookie:IsEnabled"]))
            //{
            //    app.UseCookieAuthentication(CreateCookieAuthenticationOptions(app));
            //}
        }


        private static OpenIdConnectOptions CreateOpenIdConnectAuthOptions(IConfiguration configuration)
        {
            var options = new OpenIdConnectOptions
            {
                ClientId = configuration["Authentication:OpenId:ClientId"],
                Authority = configuration["Authentication:OpenId:Authority"],
                PostLogoutRedirectUri = configuration["App:WebSiteRootAddress"] + "Account/Login",
                ResponseType = OpenIdConnectResponseType.IdToken
            };

            var clientSecret = configuration["Authentication:OpenId:ClientSecret"];
            if (!clientSecret.IsNullOrWhiteSpace())
            {
                options.ClientSecret = clientSecret;
            }

            return options;
        }

        private static JwtBearerOptions CreateJwtBearerAuthenticationOptions(IApplicationBuilder app)
        {
            var tokenAuthConfig = app.ApplicationServices.GetRequiredService<TokenAuthConfiguration>();

            return new JwtBearerOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                TokenValidationParameters = new TokenValidationParameters
                {
                    // The signing key must match!
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = tokenAuthConfig.SecurityKey,

                    // Validate the JWT Issuer (iss) claim
                    ValidateIssuer = true,
                    ValidIssuer = tokenAuthConfig.Issuer,

                    // Validate the JWT Audience (aud) claim
                    ValidateAudience = true,
                    ValidAudience = tokenAuthConfig.Audience,

                    // Validate the token expiry
                    ValidateLifetime = true,

                    // If you want to allow a certain amount of clock drift, set that here
                    ClockSkew = TimeSpan.Zero
                }
            };
        }

        private static CookieAuthenticationOptions CreateCookieAuthenticationOptions(IApplicationBuilder app)
        {
            return new CookieAuthenticationOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                AuthenticationScheme = "Cookie",
                CookieName = "access_token"
            };
        }
    }
}
