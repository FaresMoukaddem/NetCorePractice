using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace VotingAppAPI.Middleware
{
    public class RoleMiddleware
    {
        private readonly RequestDelegate _next;

        public RoleMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            string controller =httpContext.Request.RouteValues["controller"].ToString().ToLower();
            
            System.Console.WriteLine("Controller: " + controller);

            if (controller != "auth" && !IsAllowed(int.Parse(httpContext.User.Claims.ToList()[2].Value) == 1, controller))
            {
                httpContext.Response.StatusCode = 401; // Un-Authorized
                await httpContext.Response.WriteAsync("Invalid User Role");
                return;
            }
            else
            {
                await _next(httpContext);
            }
        }

        public bool IsAllowed(bool isVoter, string controller)
        {
            if(isVoter)
            {
                switch(controller)
                {
                    case "election":
                        return false;

                    default:
                        return true;
                }
            }
            else
            {
                switch(controller)
                {
                    case "voter":
                        return false;

                    default:
                        return true;
                }
            }
        }
    }
}
