using System.Runtime.CompilerServices;
using FindMyWork.Modules.Jobs.Core.Application.Common.Contracts.Database;
using FindMyWork.Modules.Jobs.Core.Application.Jobs;
using FindMyWork.Modules.Jobs.Core.Application.Jobs.Contracts;
using FindMyWork.Modules.Jobs.Core.Application.Jobs.Mappings;
using FindMyWork.Modules.Jobs.Core.Infrastructure.Persistence;
using FindMyWork.Modules.Jobs.Core.Infrastructure.Persistence.Repositories;
using FindMyWork.Shared.Infrastructure.Database.Postgres;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("FindMyWork.Modules.Jobs.Api")]
namespace FindMyWork.Modules.Jobs.Core.Application.Common.Extensions;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(JobMappingProfile));
        services.AddScoped<IJobRepository, JobRepository>();
        services.AddScoped<IJobService, JobService>();
        services.AddPostgres<ApplicationDbContext>();

        services.AddFluentValidation(fv =>
        {
            fv.RegisterValidatorsFromAssemblyContaining<ApplicationDbContext>();
            fv.DisableDataAnnotationsValidation = true;
        });
        
        return services;
    }
}