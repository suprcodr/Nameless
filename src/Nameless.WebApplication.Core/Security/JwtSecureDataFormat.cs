using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.IdentityModel.Tokens;
using Resource = Nameless.WebApplication.Core.Properties.Resources;

namespace Nameless.WebApplication.Core.Security {

    public sealed class JwtSecureDataFormat : ISecureDataFormat<AuthenticationTicket> {

        #region Private Read-Only Fields

        private readonly string _algorithm;
        private readonly TokenValidationParameters _parameters;

        #endregion Private Read-Only Fields

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of <see cref="JwtSecureDataFormat"/>.
        /// </summary>
        /// <param name="algorithm">The algorithm.</param>
        /// <param name="parameters">The validation parameters</param>
        public JwtSecureDataFormat(string algorithm, TokenValidationParameters parameters) {
            Prevent.ParameterNullOrWhiteSpace(algorithm, nameof(algorithm));
            Prevent.ParameterNull(parameters, nameof(parameters));

            _algorithm = algorithm;
            _parameters = parameters;
        }

        #endregion Public Constructors

        #region ISecureDataFormat<AuthenticationTicket> Members

        /// <inheritdoc />
        public AuthenticationTicket Unprotect(string protectedText) => Unprotect(protectedText, null);

        /// <inheritdoc />
        public AuthenticationTicket Unprotect(string protectedText, string purpose) {
            var handler = new JwtSecurityTokenHandler();
            ClaimsPrincipal claimsPrincipal = null;
            SecurityToken securityToken = null;

            try {
                claimsPrincipal = handler.ValidateToken(protectedText, _parameters, out securityToken);
                var jwtSecurityToken = securityToken as JwtSecurityToken;

                if (jwtSecurityToken == null) {
                    throw new ArgumentException(Resource.Validation_InvalidJWT);
                }

                if (!jwtSecurityToken.Header.Alg.Equals(_algorithm, StringComparison.Ordinal)) {
                    throw new ArgumentException(string.Format(Resource.Assert_TokenAlgorithm, _algorithm));
                }
            }
            catch (SecurityTokenValidationException) { return null; }
            catch (ArgumentException) { return null; }

            // Token validation passed
            return new AuthenticationTicket(claimsPrincipal, new AuthenticationProperties(), "Cookie");
        }

        /// <inheritdoc />
        public string Protect(AuthenticationTicket data) {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public string Protect(AuthenticationTicket data, string purpose) {
            throw new NotImplementedException();
        }

        #endregion ISecureDataFormat<AuthenticationTicket> Members
    }
}