using Microsoft.AspNetCore.Builder;
using VotingAppAPI.Middleware;

namespace VotingAppAPI.Helpers
{
    public static class ExtensionMethods
    {
        public static IApplicationBuilder UseRoleMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RoleMiddleware>();
        }
    }
}