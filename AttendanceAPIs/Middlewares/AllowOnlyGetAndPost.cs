namespace PMPoshanWithAngular.Server.Middlewares
{
    public class AllowOnlyGetAndPost
    {
        private readonly RequestDelegate _next;

        public AllowOnlyGetAndPost(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var method = context.Request.Method;

            if (!HttpMethods.IsGet(method) && !HttpMethods.IsPost(method))
            {
                context.Response.StatusCode = StatusCodes.Status405MethodNotAllowed;
                await context.Response.WriteAsync($"HTTP method {method} is not allowed.");
                return;
            }

            await _next(context);
        }
    }

}
