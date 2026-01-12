using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ASeric00_lab_07.Exceptions;
using System.Net;

namespace ASeric00_lab_07.Filters
{
    public class ErrorFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine($"Sent response at: {DateTime.UtcNow.ToLongTimeString()}");

            // If error is thrown
            if (context.Exception != null && !context.ExceptionHandled)
            {
                Console.WriteLine($"ERROR: {context.Exception.Message}");
                context.ExceptionHandled = true;

                string errorMessage;
                int statusCode;
                if (context.Exception.GetType() == typeof(WeatherStationAppException_UserError))
                {
                    // User error
                    statusCode = (int)HttpStatusCode.BadRequest;
                    errorMessage = context.Exception.Message;
                }
                else
                {
                    // Server error
                    statusCode = (int)HttpStatusCode.InternalServerError;
                    errorMessage = "Web API encountered an error!";
                }

                // Create the error message
                context.Result = new ContentResult
                {
                    StatusCode = statusCode,
                    ContentType = "application/text",
                    Content = errorMessage,
                };
            }

            base.OnActionExecuted(context);
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine($"Got one request at: {DateTime.UtcNow.ToLongTimeString()}");

            base.OnActionExecuting(context);
        }
    }
}