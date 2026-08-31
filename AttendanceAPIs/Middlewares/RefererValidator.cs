namespace PMPoshanWithAngular.Server.Middlewares
{
    public class RefererValidator
    {
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _environment;
        private readonly string[] _allowedDomainSuffixes;

        public RefererValidator(
            RequestDelegate next,
            IConfiguration configuration,
            IWebHostEnvironment environment)
        {
            _next = next;
            _environment = environment;

            _allowedDomainSuffixes = new[]
            {
                ".gov.in",
                ".nic.in"
            };
        }

        public async Task Invoke(HttpContext context)
        {
            if (ShouldSkipValidation(context))
            {
                await _next(context);
                return;
            }

            var referer = !string.IsNullOrWhiteSpace(context.Request.Headers.Referer)
                ? context.Request.Headers.Referer.ToString()
                : "https://miniu.gov.in";

            if (!Uri.TryCreate(referer, UriKind.Absolute, out var refererUri))
            {
                await _next(context);
                return;
            }

            var host = refererUri.Host;
            if (IsAllowedHost(host))
            {
                await _next(context);
                return;
            }

            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            await context.Response.WriteAsync("Access denied: Invalid referrer.");
        }

        private bool ShouldSkipValidation(HttpContext context)
        {
            if (_environment.IsDevelopment())
                return true;

            var path = context.Request.Path.Value ?? string.Empty;
            return path.StartsWith("/swagger", StringComparison.OrdinalIgnoreCase);
        }

        private bool IsAllowedHost(string host)
        {
            if (string.IsNullOrEmpty(host))
                return true;

            if (host.Equals("localhost", StringComparison.OrdinalIgnoreCase)
                || host.Equals("127.0.0.1", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            return _allowedDomainSuffixes.Any(
                suffix => host.EndsWith(suffix, StringComparison.OrdinalIgnoreCase));
        }
    }

}
