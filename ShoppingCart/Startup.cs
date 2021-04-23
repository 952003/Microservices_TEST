using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shed.CoreKit.WebApi;
using Middleware;
using System.Net.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Interfaces;
using Microsoft.Extensions.Logging;

namespace ShoppingCart
{
    public class Startup
    {
       
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCorrelationToken();
            services.AddCors();
            services.AddTransient<IShoppingCart, ShoppingCartImpl>();
            services.AddTransient<HttpClient>();
            services.AddWebApiEndpoints(new WebApiEndpoint<IProductCatalog>(new System.Uri("http://localhost:5001")));
            services.AddLogging(builder => builder.AddConsole());
            services.AddRequestLogging();
        }

        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCorrelationToken();
            app.UseRequestLogging("getevents");
            app.UseCors(builder =>
            {
                builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            });

            app.UseWebApiEndpoint<IShoppingCart>();
        }
    }
}
