using System;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Nameless.WebApplication.Core.Identity.Models;
using Nameless.WebApplication.Core.Security;

namespace Nameless.WebApplication {

    public partial class StartUp {

        #region Private Constants

        private const string SecretKey_Key = "TokenAuthentication:SecretKey";
        private const string Issuer_Key = "TokenAuthentication:Issuer";
        private const string Audience_Key = "TokenAuthentication:Audience";
        private const string CookieName_Key = "TokenAuthentication:CookieName";
        private const string TokenPath_Key = "TokenAuthentication:TokenPath";

        #endregion Private Constants

        #region Private Methods

        private void ConfigureAuth(IApplicationBuilder appBuilder) {
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration[SecretKey_Key]));
            var tokenValidationParameters = new TokenValidationParameters {
                // The signing key must match!
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,
                // Validate the JWT Issuer (iss) claim
                ValidateIssuer = true,
                ValidIssuer = Configuration[Issuer_Key],
                // Validate the JWT Audience (aud) claim
                ValidateAudience = true,
                ValidAudience = Configuration[Audience_Key],
                // Validate the token expiry
                ValidateLifetime = true,
                // If you want to allow a certain amount of clock drift, set that here:
                ClockSkew = TimeSpan.Zero
            };

            appBuilder.UseJwtBearerAuthentication(new JwtBearerOptions {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                TokenValidationParameters = tokenValidationParameters
            });

            appBuilder.UseCookieAuthentication(new CookieAuthenticationOptions {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                AuthenticationScheme = "Cookie",
                CookieName = Configuration[CookieName_Key],
                TicketDataFormat = new JwtSecureDataFormat(SecurityAlgorithms.HmacSha256, tokenValidationParameters)
            });

            var tokenProviderOptions = new TokenProviderOptions {
                Path = Configuration[TokenPath_Key],
                Audience = Configuration[Audience_Key],
                Issuer = Configuration[Issuer_Key],
                SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256),
                IdentityResolver = (username, password) => GetIdentity(appBuilder, username, password)
            };

            appBuilder.UseMiddleware<TokenProviderMiddleware>(Options.Create(tokenProviderOptions));
        }

        private async Task<ClaimsIdentity> GetIdentity(IApplicationBuilder appBuilder, string username, string password) {
            var userManager = appBuilder.ApplicationServices.GetService<UserManager<User>>();
            var user = await userManager.FindByNameAsync(username);

            if (user == null) { return null; }

            var signinManager = appBuilder.ApplicationServices.GetService<SignInManager<User>>();
            var result = await signinManager.CheckPasswordSignInAsync(user, password, lockoutOnFailure: true);
            
            if (result.Succeeded && !result.IsLockedOut && !result.IsNotAllowed) {
                return new ClaimsIdentity(new GenericIdentity(user.UserName, "Token"), new Claim[] {
                    new Claim("user_name", user.UserName),
                    new Claim("user_id", user.ID.ToString())
                });
            }

            // Credentials are invalid, or account doesn't exist
            return null;
        }

        #endregion Private Methods
    }
}