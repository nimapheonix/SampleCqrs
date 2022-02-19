using Newtonsoft.Json;
using SampleCqrs.Application.Exceptions;
using SampleCqrs.Domain.Exceptions;

namespace SampleCqrs.Angular.Middlewares
{
    public sealed class ExceptionHandlingMiddleware : IMiddleware
    {
        //
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        //
        public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger) => _logger = logger;
        //
        public async Task InvokeAsync(HttpContext httpContext, RequestDelegate next)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                await HandleExceptionAsync(httpContext, exception);
            }
        }
        //
        private static async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {

            var statusCode = GetStatusCode(exception);
            var response = new
            {
                title = GetTitle(exception),
                status = statusCode,
                detail = exception.Message,
                error = GetErrors(exception)
            };
            //
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = statusCode;
            await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(response));
            //
        }

        private static int GetStatusCode(Exception exception) =>
            exception switch
            {
                BadHttpRequestException => StatusCodes.Status400BadRequest,
                NotFoundException => StatusCodes.Status404NotFound,
                ValidationException => StatusCodes.Status422UnprocessableEntity,
                _ => StatusCodes.Status500InternalServerError
            };

        private static string GetTitle(Exception exception) =>
              exception switch
              {
                  DomainException applicationException => applicationException.Title,
                  _ => "Validation Error"
              };

        private static IReadOnlyDictionary<string, string[]> GetErrors(Exception exception)
        {
            IReadOnlyDictionary<string, string[]> errors = null;
            if (exception is ValidationException validationException)
            {
                errors = validationException.ErrorsDictionary;
            }
            return errors;
        }
        //
    }
}
