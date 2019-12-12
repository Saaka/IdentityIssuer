using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using IdentityIssuer.Common.Exceptions;
using Microsoft.AspNetCore.Hosting;

namespace IdentityIssuer.WebAPI.Pipeline
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly IHostingEnvironment env;

        public CustomExceptionFilterAttribute(IHostingEnvironment env)
        {
            this.env = env;
        }

        public override void OnException(ExceptionContext context)
        {
            var exception = context.Exception;
            if (exception is CommandValidationException validationException)
                HandleValidationException(validationException, context);
            else
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
            context.HttpContext.Response.StatusCode = (int) code;
            context.Result = new JsonResult(new
            {
                Error = context.Exception.Message,
                ErrorDetails = env.IsDevelopment() ? context.Exception.StackTrace : string.Empty
            });
        }

        private void HandleValidationException(CommandValidationException validationException, ExceptionContext context)
        {
            context.HttpContext.Response.ContentType = "application/json";
            context.HttpContext.Response.StatusCode = (int) HttpStatusCode.BadRequest;
            context.Result = new JsonResult(new
            {
                Error = validationException.Message,
                Errors = validationException.Errors.Select(ToCamelCase).ToList(),
                ErrorDetails = env.IsDevelopment() ? validationException.Failures : new Dictionary<string, string[]>(),
            });
        }

        private string ToCamelCase(string input)
        {
            return input.Length <= 1 ? input : $"{input.ToLowerInvariant()[0]}{input.Substring(1)}";
        }
    }
}