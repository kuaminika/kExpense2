using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kExpense.Service.Income;
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
    public partial class Startup
    {
        private string MyAllowSpecificOrigins;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
             MyAllowSpecificOrigins = "_myAllowSpecificOrigins"; 
        }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        { 
            services.AddCors(options =>
             { options.AddPolicy(name: MyAllowSpecificOrigins,
                      builder =>
                      {     Console.Out.WriteLine($"allowedOrgigins {Configuration["allowedOringins"]}"); 
                         string[] allowedOrigins = Configuration["allowedOringins"].Split(',');
                           builder.WithOrigins(allowedOrigins )
                         .AllowAnyHeader()     
                    
                         .AllowAnyMethod();
                      }); 
             });
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            Console.Out.WriteLine($"IsDevelopment: {env.IsDevelopment().ToString()}");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            } 
            app.UseForwardedHeaders(new ForwardedHeadersOptions { ForwardedHeaders = Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedFor});
            app.UseDeveloperExceptionPage();
            //if(!env.IsDevelopment())
           // app.UseHttpsRedirection();
            app.UseCors("_myAllowSpecificOrigins");
            app.UseRouting();
            app.UseAuthorization(); 
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
