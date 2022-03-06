using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace kExpense2
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        private string coorsPolicyName = "kCoorsPolicy";
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {            
            services.AddCors(options =>
             {
                 options.AddPolicy(coorsPolicyName,
                 builder =>
                 {
                     //allowedOringins came from the kExpenseConfig.json
                     string[] allowedOrigins = Configuration["allowedOringins"].Split(',');
                     builder.WithOrigins(allowedOrigins)
                     .AllowAnyHeader()                      
                     .AllowAnyMethod();
                 });
             });
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {/*
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }*/
           // TODO need to change json config file if env.IsDev
            app.UseDeveloperExceptionPage();
            app.UseHttpsRedirection();
            app.UseCors(coorsPolicyName);
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
