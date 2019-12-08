using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Net;

namespace IdentityIssuer.WebAPI.Pipeline
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var exception = context.Exception;
            HandleApplicationExceptions(context, exception);
        }

        private void HandleApplicationExceptions(ExceptionContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;

            switch (exception)
            {
                case ArgumentException _:
                    code = HttpStatusCode.BadRequest;
                    break;
                case InvalidOperationException _:
                    code = HttpStatusCode.Forbidden;
                    break;
                case UnauthorizedAccessException _:
                    code = HttpStatusCode.Unauthorized;
                    break;
            }

            context.HttpContext.Response.ContentType = "application/json";
            context.HttpContext.Response.StatusCode = (int)code;
            context.Result = new JsonResult(new
            {
                Error = context.Exception.Message,
                ErrorDetails = context.Exception.StackTrace
            });
        }
    }
}
