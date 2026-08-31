namespace PMPoshanWithAngular.Server.Middlewares
{
    public class ApplyCSP
    {
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _environment;

        public ApplyCSP(RequestDelegate next, IWebHostEnvironment environment)
        {
            _next = next;
            _environment = environment;
        }

        public async Task Invoke(HttpContext context)
        {
            var path = context.Request.Path.Value ?? string.Empty;
            var isSwagger = path.StartsWith("/swagger", StringComparison.OrdinalIgnoreCase);

            if (!_environment.IsDevelopment() && !isSwagger)
            {
                context.Response.Headers["X-Frame-Options"] = "SAMEORIGIN";
                context.Response.Headers["Content-Security-Policy"] =
                    "default-src 'self';" +
                    "font-src 'self' https://fonts.gstatic.com;" +
                    "img-src 'self' data:;" +
                    "script-src 'self' https://www.googletagmanager.com;" +
                    "style-src 'self' https://fonts.googleapis.com 'unsafe-inline';" +
                    "connect-src 'self' https://fonts.googleapis.com;" +
                    "object-src 'none';" +
                    "frame-ancestors 'none';";
            }

            await _next(context);
        }
    }

}
