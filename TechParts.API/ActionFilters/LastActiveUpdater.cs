using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using TechParts.API.Data;
using Microsoft.Extensions.DependencyInjection;

namespace TechParts.API.ActionFilters
{
    public class LastActiveUpdater : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            try
            {
            var resultContext = await next();

            var userId = int.Parse(resultContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var repo = resultContext.HttpContext.RequestServices.GetService<IUserRepositroy>();

            var user = await repo.GetUser(userId);

            user.lastActive = DateTime.Now;

            await repo.SaveDatabase();
            }
            catch (Exception e)
            {
                System.Console.WriteLine("------------------------------------");
                System.Console.WriteLine("Couldnt update last active:");
                System.Console.WriteLine(e.Message);
                System.Console.WriteLine("------------------------------------");
            }
        }
    }
}