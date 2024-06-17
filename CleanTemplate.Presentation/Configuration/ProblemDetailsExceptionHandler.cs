using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace CleanTemplate.Presentation
{
    public class ProblemDetailsExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            ProblemDetails problemDetails = new ProblemDetails
            {
                Title = "An error occurred",
                Detail = exception.Message,
                Type = exception.GetType().Name,
                Status = (int)HttpStatusCode.InternalServerError
            };

            var badRequestExceptions = new[] { typeof(ValidationException), typeof(ArgumentOutOfRangeException), typeof(ArgumentNullException) };

            if (badRequestExceptions.Contains(exception.GetType()))
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                problemDetails = new ProblemDetails
                {
                    Title = "Bad Request",
                    Detail = $"One or more errors occurred. {exception.Message}",
                    Type = exception.GetType().Name,
                    Status = (int)HttpStatusCode.BadRequest
                };
            }

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken: cancellationToken);

            return true;
        }
    }
}
