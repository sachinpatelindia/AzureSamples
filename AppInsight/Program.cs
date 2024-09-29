using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.ApplicationInsights;
namespace AppInsight
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            //Add App insights telemetry
            builder.Services.AddApplicationInsightsTelemetry(builder.Configuration["ApplicationInsights:ConnectionString"]);
            var conn = builder.Configuration["ApplicationInsights:ConnectionString"];
            //Configure logging
            builder.Logging.AddApplicationInsights(configureTelemetryConfiguration: (config) =>

                
                    config.ConnectionString = builder.Configuration["ApplicationInsights:ConnectionString"],
                configureApplicationInsightsLoggerOptions: (options) => { }
                
            );
            // builder.Logging.AddFilter<ApplicationInsightsLoggerProvider>("AzureSamples-Trace", LogLevel.Trace);

            IServiceProvider serviceProvider = builder.Services.BuildServiceProvider();
            ILogger<Program> logger = serviceProvider.GetRequiredService<ILogger<Program>>();

            logger.LogInformation("Logger is working...");
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
