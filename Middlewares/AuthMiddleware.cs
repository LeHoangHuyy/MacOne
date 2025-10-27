using Macone.Middlewares;

namespace Macone.Middlewares
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path;

            // Allow Url
            if (path.StartsWithSegments("/css") ||
                path.StartsWithSegments("/js") ||
                path.StartsWithSegments("/images") ||
                path.StartsWithSegments("/lib") ||    
                path.StartsWithSegments("/error") ||
                path.StartsWithSegments("/user-assets") ||
                path.StartsWithSegments("/admin-assets"))
            {
                await _next(context);
                return;
            }

            // Allow ROOT, LOGIN, REGISTER, or other public actions
            if (path.Equals("/Account/Login", System.StringComparison.OrdinalIgnoreCase) ||
                path.Equals("/Account/Register", System.StringComparison.OrdinalIgnoreCase) ||
                path.Equals("/", System.StringComparison.OrdinalIgnoreCase))
            {
                await _next(context);
                return;
            }

            // Check session / cookie role
            var role = context.Session.GetString("Role");
            var cookieRole = context.Request.Cookies["Role"];
            var userRole = !string.IsNullOrEmpty(role) ? role : cookieRole;

            // If not role => redirect to login
            if (string.IsNullOrEmpty(userRole))
            {
                // If the request is /Account/Login, do not redirect anymore
                if (path.Equals("/Account/Login", System.StringComparison.OrdinalIgnoreCase))
                {
                    await _next(context);
                    return;
                }

                context.Response.Redirect("/Account/Login");
                return;
            }

            // If has role, check tra role-based path
            if (path.StartsWithSegments("/admin") && userRole != "ADMIN")
            {
                context.Response.Redirect("/Account/Login");
                return;
            }

            if (path.StartsWithSegments("/user") && userRole != "USER")
            {
                context.Response.Redirect("/Account/Login");
                return;
            }

            await _next(context);
        }
    }

    public static class AuthMiddlewareExtensions
    {
        public static IApplicationBuilder UseAuthMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthMiddleware>();
        }
    }
}