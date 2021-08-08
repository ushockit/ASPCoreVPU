using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Middlewares
{
    public class AuthMiddleware
    {
        readonly RequestDelegate _next;
        public AuthMiddleware(RequestDelegate next, string defaultLogin, string defaultPassword)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext ctx)
        {
            string username = ctx.Request.Cookies["username"];
            if (!string.IsNullOrWhiteSpace(username))
            {
                await _next.Invoke(ctx);
            }

            await ctx.Response.WriteAsync("Please, authorize!");
        }
    }
}
