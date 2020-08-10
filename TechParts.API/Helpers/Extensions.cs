

using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace TechParts.API.Helpers
{
    public static class Extensions
    {
        public static void AddPagination(this HttpResponse response, PagedParams pagedParams)
        {
            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };

            response.Headers.Add("Pagination", JsonSerializer.Serialize(pagedParams, jsonOptions));
            response.Headers.Add("Access-Control-Expose-Headers","Pagination");
        }

        public static void AddErrorResponse(this HttpResponse response, string message)
        {
            response.Headers.Add("Application-Error", message);

            response.Headers.Add("Access-Control-Expose-Headers", "Application-Error");
            response.Headers.Add("Access-Control-Allow-Origin", "*");
        }
    }
}