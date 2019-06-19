using System;
using System.Net;
using System.Threading.Tasks;

using LogParser.DataModels.Models;

using Microsoft.AspNetCore.Http;

using Newtonsoft.Json;

namespace LogParser.Server.MiddleWares
{
    public class UnhandledExceptionMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception e)
            {
                await BuildExceptionResponse(context, BuildErrorResponse(e), HttpStatusCode.BadRequest);
            }
        }

        private static async Task BuildExceptionResponse(HttpContext context, string message, HttpStatusCode statusCode)
        {
            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(message);
        }

        private string BuildErrorResponse(Exception e)
        {
            return JsonConvert.SerializeObject(new ClientResponseError{Error = e.Message});
        }
    }
}