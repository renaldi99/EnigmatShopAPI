﻿namespace EnigmatShopAPI.Middlewares
{
    public class HandleExceptionMiddleware : IMiddleware
    {
        public Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            throw new NotImplementedException();
        }
    }
}
