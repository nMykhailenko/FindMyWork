using FIndMyWork.Modules.Jobs.Api;
using FindMyWork.Shared.Infrastructure.Extensions;

namespace FindMyWork.Modular.API;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSharedInfrastructure();
        services.AddJobsModule();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseSharedInfrastructure();
        
        app.UseRouting();
        
        app.UseJobsModule();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapGet("/", 
                context => context.Response.WriteAsync("FindMyWork API"));
        });
    }
}