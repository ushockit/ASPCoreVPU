using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Middlewares;
using WebApplication1.Services;

namespace WebApplication1
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDistributedMemoryCache();
            services.AddSession();


            // Регистрация сервиса
            services.AddSingleton<IFileHelper, FileHelper>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app,
            IWebHostEnvironment env,
            IFileHelper fileHelper)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSession();
            app.UseRouting();


            StringBuilder textResp = new StringBuilder();

            app.Use(async (ctx, next) =>
            {
                if (ctx.Request.Cookies["lang"] is null)
                {
                    ctx.Response.Cookies.Append("lang", "en");
                }
                await next.Invoke();
            });
            app.Use(async (context, next) =>
            {
                string time = context.Session.GetString("timeLogIn");
                textResp.Clear();

                textResp.Append("<h1>Text from Middleware</h1>");
                // переход дальше по цепочке запроса
                await next.Invoke();
            });
            // app.UseMiddleware<AuthMiddleware>("admin", "admin");

            app.Map("/home", (home) =>
            {
                home.Run(async (ctx) =>
                {
                    ctx.Session.SetString("timeLogIn", DateTime.Now.ToString());
                    await ctx.Response.WriteAsync("<h1>Home page</h1>");
                });
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    textResp.Append("<h2>Text from endpoint</h2>");
                    await context.Response.WriteAsync(textResp.ToString());

                });
            });
        }
    }
}
