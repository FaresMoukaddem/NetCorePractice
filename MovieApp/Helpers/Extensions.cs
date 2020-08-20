using Microsoft.AspNetCore.Identity;

namespace MovieApp.Helpers
{
    public static class ExtensionMethods
    {
        public static string GetErrorString(this IdentityResult identityRes)
        {
            string errorString = string.Empty;

            foreach(IdentityError err in identityRes.Errors)
            {
                errorString += err.Description + "\n";
            }

            return errorString;
        }
    }
}