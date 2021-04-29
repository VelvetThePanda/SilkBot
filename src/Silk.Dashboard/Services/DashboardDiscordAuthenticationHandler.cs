using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;
using AspNet.Security.OAuth.Discord;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Silk.Dashboard.Services
{
    /*
     * Adapted from: https://github.com/GedasFX/Alderto/blob/master/AspNet.Security.OAuth.Discord/DiscordAuthenticationHandler.cs
     * Credit: GedasFX/Alderto
     */
    public class DashboardDiscordAuthenticationHandler : DiscordAuthenticationHandler
    {
        public DashboardDiscordAuthenticationHandler(
            IOptionsMonitor<DiscordAuthenticationOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock)
            : base(options, logger, encoder, clock)
        {
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            /* Todo: Check if result can ever be NULL */
            var result = await Context.AuthenticateAsync(SignInScheme);
            if (result is null)
                return AuthenticateResult.Fail("Remote Authentication does not directly support AuthenticateAsync");

            if (result.Failure is not null)
                return result;

            var ticket = result.Ticket;
            if (TicketInfoAvailable(ticket) && OAuthProviderIsDiscord(ticket))
            {
                /* Todo: Check that DateTime from Token is LocalTime */
                var expirationDateTime = DateTime.Parse(ticket.Properties.GetTokenValue("expires_at"), CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);
                var timeDiff = expirationDateTime - Clock.UtcNow.LocalDateTime;

                // Try to refresh access if token will expire in less than 2 hours
                if (timeDiff.TotalHours < 2)
                {
                    if (await TryRefreshTokenAsync(ticket))
                    {
                        await Context.SignInAsync(SignInScheme, ticket.Principal, ticket.Properties);
                    }
                    else
                    {
                        return AuthenticateResult.Fail("Token Refresh Failed");
                    }
                }

                return AuthenticateResult.Success(new AuthenticationTicket(ticket.Principal, ticket.Properties, Scheme.Name));
            }

            return AuthenticateResult.Fail("Not Authenticated");
        }

        // The SignInScheme may be shared with multiple providers, make sure this provider issued the identity.
        private bool OAuthProviderIsDiscord(AuthenticationTicket? ticket)
        {
            return ticket is not null &&
                   ticket.Properties.Items.TryGetValue(".AuthScheme", out var authenticatedScheme) &&
                   string.Equals(Scheme.Name, authenticatedScheme, StringComparison.Ordinal);
        }

        private bool TicketInfoAvailable(AuthenticationTicket? ticket)
        {
            return ticket is {Principal: { }, Properties: { }};
        }

        private async Task<bool> TryRefreshTokenAsync(AuthenticationTicket ticket)
        {
            var refreshToken = ticket.Properties.GetTokenValue("refresh_token");

            var tokenRequestParameters = new Dictionary<string, string>
            {
                {"client_id", Options.ClientId},
                {"client_secret", Options.ClientSecret},
                {"grant_type", "refresh_token"},
                {"refresh_token", refreshToken},
            };

            var requestContent = new FormUrlEncodedContent(tokenRequestParameters);

            using var requestMessage = new HttpRequestMessage(HttpMethod.Post, Options.TokenEndpoint);
            requestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            requestMessage.Content = requestContent;

            var response = await Backchannel.SendAsync(requestMessage, Context.RequestAborted);
            
            if (response.IsSuccessStatusCode)
            {
                await using var stream = await response.Content.ReadAsStreamAsync();
                var payload = await JsonDocument.ParseAsync(stream);
                var tokens = OAuthTokenResponse.Success(payload);

                ticket.Properties.UpdateTokenValue("access_token", tokens.AccessToken);
                ticket.Properties.UpdateTokenValue("refresh_token", tokens.RefreshToken);

                if (int.TryParse(tokens.ExpiresIn, NumberStyles.Integer, CultureInfo.InvariantCulture, out var value))
                {
                    var expiresAt = Clock.UtcNow + TimeSpan.FromSeconds(value);
                    ticket.Properties.UpdateTokenValue("expires_at", expiresAt.ToString("o", CultureInfo.InvariantCulture));
                }

                return true;
            }

            return false;
        }
    }
}