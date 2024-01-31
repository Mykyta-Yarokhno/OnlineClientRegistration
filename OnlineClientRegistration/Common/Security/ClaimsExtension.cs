using System.Security.Claims;

namespace OnlineClientRegistration.Common.Security
{
    public static class ClaimsExtension
    {
        public static string GetMobilePhone(this ClaimsPrincipal claims)
        {
            return claims.FindFirst(ClaimTypes.MobilePhone)?.Value;
        }

        public static bool IsAdmin(this ClaimsPrincipal claims)
        {
            return claims.FindFirst(ClaimTypes.Role)?.Value == AccessRoles.Admin;
        }

        public static bool IsManager(this ClaimsPrincipal claims)
        {
            return claims.FindFirst(ClaimTypes.Role)?.Value == AccessRoles.Manager;
        }
    }
}
