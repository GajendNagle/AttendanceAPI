using Microsoft.AspNetCore.Mvc.Filters;

namespace PMPoshanWithAngular.Server.Filters.Security
{
    public class ResponseHeaderFilter : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            context.HttpContext.Response.Headers.Add("server", "wish i knew");
            context.HttpContext.Response.Headers.Add("X-Powered-By", "curosity");

            base.OnResultExecuting(context);
        }
    }
}
