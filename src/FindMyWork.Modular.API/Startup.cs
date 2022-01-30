using FIndMyWork.Modules.Jobs.Api;

namespace FindMyWork.Modular.API;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddJobsModule();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
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