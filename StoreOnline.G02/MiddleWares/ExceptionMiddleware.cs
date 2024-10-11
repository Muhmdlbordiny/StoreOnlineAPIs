﻿using StoreOnline.G02.Error;
using System.Text.Json;

namespace StoreOnline.G02.MiddleWares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next,ILogger<ExceptionMiddleware>logger,IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);

            }catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                var response = _env.IsDevelopment() ?
                    new ApiExceptionResponse(StatusCodes.Status500InternalServerError,
                    ex.Message, ex.StackTrace.ToString() )
                    : new ApiExceptionResponse(StatusCodes.Status500InternalServerError);
                var json = JsonSerializer.Serialize(response);

                await context.Response.WriteAsync(json); //jsnstring
            }

        }
    }
}