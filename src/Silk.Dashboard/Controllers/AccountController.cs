using System.Threading.Tasks;
using AspNet.Security.OAuth.Discord;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace Silk.Dashboard.Controllers
{
    [Route("api/silk/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        [HttpGet("login")]
        [HttpPost("login")]
        public IActionResult Login(string returnUrl = "/")
        {
            var challenge = Challenge(new AuthenticationProperties {RedirectUri = returnUrl}, DiscordAuthenticationDefaults.AuthenticationScheme);
            return challenge;
        }

        [HttpGet("logout")]
        [HttpPost("logout")]
        public async Task<IActionResult> LogOut(string returnUrl = "/")
        {
            // This removes the cookie assigned to the user login.
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return LocalRedirect(returnUrl);
        }
    }
}