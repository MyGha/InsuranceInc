using System;
using System.Text;
using System.Text.Encodings.Web;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using InsuranceInc.Core.Entities;
using InsuranceInc.Business.Services;

namespace InsuranceInc.Business.Helpers
{
    //
    // The basic authentication handler is asp.net core middleware that handles request authentication by
    // inheriting from the asp.net core AuthenticationHandler base class and overriding the HandleAuthenticateAsync() method.
    //
    // Basic authentication logic is implemented in the HandleAuthenticateAsync() method by verifying the username and password
    // received in the HTTP Authorization header, verification is done by calling _userService.Authenticate(username, password).
    // On successful authentication the method returns AuthenticateResult.Success(ticket) which makes the request authenticated
    // and sets the HttpContext.User to the currently logged in user.
    //
    // The basic authentication middleware is configured in the application inside the ConfigureServices(IServiceCollection services)
    // method in the application Startup file below.
    //
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IUserService _userService;

        public BasicAuthenticationHandler(
           IOptionsMonitor<AuthenticationSchemeOptions> options,
           ILoggerFactory logger,
           UrlEncoder encoder,
           ISystemClock clock,
           IUserService userService)
           : base(options, logger, encoder, clock)
        {
            _userService = userService;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return AuthenticateResult.Fail("Missing Authorization Header");
            }

            User user = null;

            try
            {
                var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
                var credentials = Encoding.UTF8.GetString(credentialBytes).Split(new[] { ':' }, 2);
                var username = credentials[0];
                var password = credentials[1];
                
                user = await _userService.Authenticate(username, password);
            }
            catch
            {
                return AuthenticateResult.Fail("Invalid Authorization Header");
            }

            if (user == null)
            {
                return AuthenticateResult.Fail("Invalid Username or Password");
            }

            var claims = new[] {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }
    }
}
