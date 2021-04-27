using System;

namespace Silk.Dashboard.Models
{
    public record DiscordOAuthToken(string AccessToken, string RefreshToken, DateTimeOffset? ExpiresIn);
}