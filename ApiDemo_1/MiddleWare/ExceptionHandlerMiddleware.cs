using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace ApiDemo_1.MiddleWare
{
    public class ExceptionHandlerMiddleware : IMiddleware
    {
        //private readonly RequestDelegate next;

        //public ExceptionHandlerMiddleware(RequestDelegate next)
        //{
        //    this.next = next;
        //}

        //public async Task Invoke(HttpContext context)
        //{
        //    try
        //    {
        //        await next(context);
        //    }
        //    catch (Exception ex)
        //    {
        //        await HandleExceptionAsync(context, ex);
        //    }
        //}

        //private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        //{
        //    var statusCode = HttpStatusCode.InternalServerError;
        //    var message = "An error occurred.";

        //    // You can customize the status code and message based on the exception type
        //    // and perform any additional handling/logging if needed

        //    var response = new { message = message };

        //    var payload = JsonConvert.SerializeObject(response);
        //    context.Response.ContentType = "application/json";
        //    context.Response.StatusCode = (int)statusCode;

        //    // If the response status code is 404, handle the "Not Found" error
        //    if (context.Response.StatusCode == 404 && !context.Response.HasStarted)
        //    {
        //        // Clear the existing response
        //        context.Response.Clear();

        //        // Customize the "Not Found" page or response
        //        context.Response.StatusCode = 404;
        //        context.Response.ContentType = "text/html";
        //        await context.Response.WriteAsync("<h1>404 - Not Found</h1>");
        //    }

        //return context.Response.WriteAsync(payload);


        private readonly ILogger logger;

        public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger)
        {
            this.logger = logger;
        }

        //public async Task Invoke(HttpContext context)
        //{
        //    try
        //    {
        //        await next(context);
        //    }
        //    catch (CustomException ex)
        //    {
        //        await HandleCustomExceptionAsync(context, ex);
        //    }
        //    catch (Exception ex)
        //    {
        //        await HandleExceptionAsync(context, ex);
        //    }
        //}

        //private static Task HandleCustomExceptionAsync(HttpContext context, CustomException exception)
        //{
        //    var statusCode = exception.StatusCode;
        //    var message = exception.Message;

        //    var response = new { message = message };

        //    var payload = JsonConvert.SerializeObject(response);
        //    context.Response.ContentType = "application/json";
        //    context.Response.StatusCode = statusCode;

        //    return context.Response.WriteAsync(payload);
        //}

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);

                // If the response status code is 404, handle the "Page not found" error
                if (context.Response.StatusCode == 404 && !context.Response.HasStarted)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;

                    ProblemDetails problem = new ProblemDetails
                    {
                        Status = (int)HttpStatusCode.NotFound,
                        Title = "Page not found",
                        Detail = "The requested page could not be found."
                    };

                    string jsonData = JsonConvert.SerializeObject(problem);
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(jsonData);
                }
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                ProblemDetails problem = new()
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Type = "Internal Server Error",
                    Title = "Server error",
                    Detail = e.Message
                };

                string JsonData = JsonConvert.SerializeObject(problem);

                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(JsonData);
            }
            
        }
        
      
    }
    
}
