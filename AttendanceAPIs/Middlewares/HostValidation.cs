namespace PMPoshanWithAngular.Server.Middlewares
{
    public class HostValidation
    {
        private readonly RequestDelegate _next;
        private readonly string[] _allowedHosts;

        public HostValidation(RequestDelegate next, IConfiguration config)
        {
            _next = next;

            _allowedHosts = config.GetSection("AllowedHostNames").Get<string[]>()
                ?? new[] { "localhost", "127.0.0.1", "10.131.11.111" };
        }

        public async Task Invoke(HttpContext context)
        {
            var requestHost = context.Request.Host.Host;

            // Wildcard "*" ko check karein ya exact host match karein
            bool isAllowed = _allowedHosts.Contains("*") ||
                             _allowedHosts.Contains(requestHost, StringComparer.OrdinalIgnoreCase);

            if (!isAllowed)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync($"Invalid Host Header: {requestHost}");
                return;
            }

            await _next(context);
        }
    }
}
