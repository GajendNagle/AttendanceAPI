namespace PMPoshanWithAngular.Server.Helper
{
    public class AuthHelper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AuthHelper(IHttpContextAccessor httpContextAccessor  )
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public string GetIPAddress()
        {
            var httpContext = _httpContextAccessor.HttpContext;

            if (httpContext == null)
                return "Unknown";

            string ipAddress = httpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault();

            if (string.IsNullOrEmpty(ipAddress))
            {
                ipAddress = httpContext.Connection.RemoteIpAddress?.ToString();
                if (ipAddress == "::1")
                {
                    ipAddress = "127.0.0.1"; // Localhost IP for development
                }
            }

            return ipAddress ?? "Unknown";
        }
    }
}
