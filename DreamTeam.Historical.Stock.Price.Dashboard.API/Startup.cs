using DreamTeam.Historical.Stock.Price.Dashboard.Application.Models;
using DreamTeam.Historical.Stock.Price.Dashboard.Application.Services;
using Microsoft.OpenApi.Models;

namespace DreamTeam.Historical.Stock.Price.Dashboard.API;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    private IConfiguration Configuration { get; }


    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddApiVersioning();
        services.Configure<ConfigSettings>(options =>
            Configuration.GetSection("ConfigSettings").Bind(options));

        // DI SignalR
        services.AddSignalR();
        
        services.AddHostedService<DailyPricesHostedServices>();
        
        
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "DreamTeam Historical Stock Price Dashboard Microservice API",
                Version = "v1.0",
            });
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DreamTeam.Historical.Stock.Price.Dashboard.API v1"));
        }
        
        
        app.UseRouting();
            
        app.UseCors(builder => builder
            .AllowCredentials()
            .AllowAnyMethod()
            .AllowAnyHeader()
            .WithOrigins("http://localhost:4200"));

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapHub<SignalService>("SignalService");
            endpoints.MapControllers();
        });
    }
}