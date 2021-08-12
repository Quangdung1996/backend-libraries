using System;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PansyDev.Common.Infrastructure.Authentication.Options;

namespace PansyDev.Common.Infrastructure.Authentication.Services
{
    internal class CommonAuthenticationHandler : AuthenticationHandler<JwtBearerOptions>
    {
        private readonly InvalidationManager _invalidationManager;
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler = new();
        private readonly SecurityOptions _securityOptions;

        public CommonAuthenticationHandler(IOptionsMonitor<JwtBearerOptions> options, ILoggerFactory logger,
            UrlEncoder encoder, ISystemClock clock, InvalidationManager invalidationManager,
            IOptions<SecurityOptions> securityOptions) : base(options, logger, encoder, clock)
        {
            _invalidationManager = invalidationManager;
            _securityOptions = securityOptions.Value;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.TryGetValue("Authorization", out var token) || token.Count != 1)
            {
                return AuthenticateResult.NoResult();
            }

            if (!token[0].StartsWith("Bearer "))
            {
                return AuthenticateResult.Fail("Invalid access token");
            }

            try
            {
                var claimsPrincipal = _jwtSecurityTokenHandler.ValidateToken(token[0].Replace("Bearer ", ""),
                    Options.TokenValidationParameters, out var validatedToken);

                if (claimsPrincipal is null)
                {
                    return AuthenticateResult.Fail("Invalid access token");
                }

                var sessionId = claimsPrincipal.FindFirst(CommonClaimTypes.SessionId);
                Debug.Assert(sessionId is not null);

                var invalidationDate = await _invalidationManager.GetInvalidationDate(Guid.Parse(sessionId.Value));

                if (invalidationDate is not null)
                {
                    var validFrom = validatedToken.ValidTo - _securityOptions.AccessTokenLifeTime;

                    if (invalidationDate.Value > validFrom)
                    {
                        return AuthenticateResult.Fail("Access token expired");
                    }
                }

                var ticket = new AuthenticationTicket(claimsPrincipal, CommonAuthenticationScheme.CustomJwtBearer);
                return AuthenticateResult.Success(ticket);
            }
            catch (Exception exception)
            {
                return AuthenticateResult.Fail(exception);
            }
        }
    }
}
