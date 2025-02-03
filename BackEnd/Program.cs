using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BackEnd
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureServices(services =>
                    {
                        services.AddSingleton<JsonService>();  // Register JsonService
                        services.AddControllers();  // Register controllers

                        // Register and enable CORS globally
                        services.AddCors(options =>
                        {
                            options.AddPolicy("AllowAll", builder =>
                            {
                                builder.AllowAnyOrigin()    // Allow any origin
                                       .AllowAnyMethod()    // Allow any HTTP method
                                       .AllowAnyHeader();   // Allow any header
                            });
                        });
                    })
                    .Configure((context, app) =>  // Use the 'context' here
                    {
                        var env = context.HostingEnvironment;  // Access IWebHostEnvironment

                        if (env.IsDevelopment())  // Check environment
                        {
                            app.UseDeveloperExceptionPage();  // Use developer page in development
                        }

                        app.UseCors("AllowAll");  // Apply the "AllowAll" CORS policy globally
                        app.UseRouting();  // Use routing

                        // Map controllers
                        app.UseEndpoints(endpoints =>
                        {
                            endpoints.MapControllers();
                        });
                    });
                });
    }
}
