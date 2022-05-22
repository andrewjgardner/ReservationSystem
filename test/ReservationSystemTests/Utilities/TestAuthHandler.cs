using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace ReservationSystemTests.Utilities
{
    public class TestAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public const string AuthenticationScheme = "Test";

        public TestAuthHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
                : base(options, logger, encoder, clock)
        {
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var ticket = await SignIn();
            return await Task.FromResult(AuthenticateResult.Success(ticket));
        }

        private async Task<AuthenticationTicket> SignIn()
        {
            var claims = new List<Claim>() {
                new Claim(ClaimTypes.NameIdentifier , "1"),
                new Claim(ClaimTypes.Name, "manager@manager.com"),
                new Claim(ClaimTypes.Email, "manager@manager.com"),
                new Claim(ClaimTypes.Role, "Manager")
            };

            var identity = new ClaimsIdentity(claims, AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            return new AuthenticationTicket(principal, AuthenticationScheme);
        }
    }
}
