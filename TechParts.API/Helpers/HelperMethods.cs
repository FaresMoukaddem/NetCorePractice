

using Microsoft.AspNetCore.Hosting;

namespace TechParts.API.Helpers
{
    public class HelperMethods
    {

        public static IWebHostEnvironment _hostEnv;

        public static string GetExtension(string contentType)
        {
            contentType = contentType.ToLower();

            if(contentType.Contains("jpeg")) return ".jpg";
            else if(contentType.Contains("png")) return ".png";
            else return string.Empty;
        }

        public static string GetHostPath()
        {
            return _hostEnv.ContentRootPath;
        }

        public static string GetImagePath(string imageName)
        {
            return GetHostPath() + "/Images/" + imageName;
        }
    }
}