namespace Backend.Server.Middleware;

public class GlobalExceptionHandler(
    RequestDelegate next,
    ILogger<GlobalExceptionHandler> logger,
    IWebHostEnvironment env)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        logger.LogError(exception, "An unhandled exception occurred: {Message}", exception.Message);

        var message = exception.Message;
        var response = context.Response;
        response.ContentType = "application/json";

        switch (exception)
        {
            case UserInputException userInputEx:
                response.StatusCode = (int)userInputEx.StatusCode;
                break;

            case ConflictException conflictEx:
                response.StatusCode = (int)conflictEx.StatusCode;
                break;

            case ForbiddenException forbiddenEx:
                response.StatusCode = (int)forbiddenEx.StatusCode;
                break;

            case InvalidOperationException:
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                break;

            default:
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                if (!env.IsDevelopment())
                    message = "An unexpected error occurred. Please try again later.";
                break;
        }

        await response.WriteAsync(message);
    }
}
