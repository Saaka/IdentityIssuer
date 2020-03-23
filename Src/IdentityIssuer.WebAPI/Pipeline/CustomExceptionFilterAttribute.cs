using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using System.Net;
using IdentityIssuer.Common.Enums;
using IdentityIssuer.Common.Exceptions;
using IdentityIssuer.WebAPI.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace IdentityIssuer.WebAPI.Pipeline
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly IWebHostEnvironment _env;

        public CustomExceptionFilterAttribute(IWebHostEnvironment env)
        {
            _env = env;
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
                Error = domainException.ErrorCode.ToString(),
                ErrorCode = domainException.ErrorCode,
                ErrorDetails = domainException.ErrorDetails
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
                ErrorDetails = !_env.IsProduction() ? context.Exception.StackTrace : string.Empty
            });
        }

        private void HandleValidationException(CommandValidationException validationException, ExceptionContext context)
        {
            context.HttpContext.Response.StatusCode = (int) HttpStatusCode.BadRequest;
            context.Result = new JsonResult(new ErrorResponse(
                code: ErrorCode.ValidationError,
                details: validationException.Failures
                    .Select(f => new {field = f.Key, errors = f.Value}))
            );
        }

        private void HandleRepositoryException(RepositoryException repositoryException, ExceptionContext context)
        {
            context.HttpContext.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
            context.Result = new JsonResult(new
            {
                Error = repositoryException.Message,
                ErrorDetails = repositoryException.Errors
            });
        }
    }
}