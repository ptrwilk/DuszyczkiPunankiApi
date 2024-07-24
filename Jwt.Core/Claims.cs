using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Jwt.Core;

public static class Claims
{
    public static string IsLobbyOwnerClaim = "IsLobbyOwnerClaim";

    public static Guid? GetUserId(this ClaimsPrincipal claims)
    {
        var userId = claims.FindFirst(JwtRegisteredClaimNames.Name);

        return string.IsNullOrEmpty(userId?.Value) ? null : Guid.Parse(userId.Value);
    }
}