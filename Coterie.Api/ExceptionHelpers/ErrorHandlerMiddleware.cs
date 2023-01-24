using System;
using System.Net;
using System.Threading.Tasks;
using Coterie.Api.Models.Responses;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace Coterie.Api.ExceptionHelpers
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var response = context.Response;

            var appContext = context.Features.Get<IExceptionHandlerPathFeature>();

            response.StatusCode = appContext.Error switch
            {
                IndexOutOfRangeException or NullReferenceException or ArgumentException => (int) HttpStatusCode.BadRequest,
                _ => (int) HttpStatusCode.InternalServerError
            };

            var ex = new BaseExceptionResponse
            {
                Message = appContext.Error.Message
            };

            await response.WriteAsJsonAsync(ex);
        }
    }
}
