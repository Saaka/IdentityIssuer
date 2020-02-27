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
            context.HttpContext.Response.ContentType = "application/json";
            
            switch (exception)
            {
                case DomainException domainException:
                    HandleDomainException(domainException, context);
                    break;
                case CommandValidationException validationException:
                    HandleValidationException(validationException, context);
                    break;
                case RepositoryException repositoryException:
                    HandleRepositoryException(repositoryException, context);
                    break;
                default:
                    HandleApplicationExceptions(context, exception);
                    break;
            }
        }

        private void HandleDomainException(DomainException domainException, ExceptionContext context)
        {
            context.HttpContext.Response.StatusCode = (int) HttpStatusCode.BadRequest;
            context.Result = new JsonResult(new
            {
                Error = domainException.Message,
                ErrorDetails = domainException.ExceptionDetails
            });
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

            context.HttpContext.Response.StatusCode = (int) code;
            context.Result = new JsonResult(new
            {
                Error = context.Exception.Message,
                ErrorDetails = env.IsDevelopment() ? context.Exception.StackTrace : string.Empty
            });
        }

        private void HandleValidationException(CommandValidationException validationException, ExceptionContext context)
        {
            context.HttpContext.Response.StatusCode = (int) HttpStatusCode.BadRequest;
            context.Result = new JsonResult(new
            {
                Error = validationException.Message,
                ErrorDetails = validationException.Failures
                    .SelectMany(x=> x.Value).Select(ToCamelCase).ToList()
            });
        }

        private void HandleRepositoryException(RepositoryException repositoryException, ExceptionContext context)
        {
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Result = new JsonResult(new
            {
                Error = repositoryException.Message,
                ErrorDetails = repositoryException.Errors
            });
        }

        private string ToCamelCase(string input)
        {
            return input.Length <= 1 ? input : $"{input.ToLowerInvariant()[0]}{input.Substring(1)}";
        }
    }
}