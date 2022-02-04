using FindMyWork.Modules.Files.Api;
using FIndMyWork.Modules.Jobs.Api;
using FindMyWork.Shared.Infrastructure.Extensions;

namespace FindMyWork.Modular.API;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSharedInfrastructure();
        services.AddJobsModule();
        services.AddFilesModule();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseSharedInfrastructure();
        
        app.UseRouting();
        
        app.UseJobsModule();
        app.UseFilesModule();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapGet("/", 
                context => context.Response.WriteAsync("FindMyWork API"));
        });
    }
}