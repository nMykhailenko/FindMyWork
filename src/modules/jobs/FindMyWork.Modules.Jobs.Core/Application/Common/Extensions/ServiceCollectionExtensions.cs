using System.Runtime.CompilerServices;
using FindMyWork.Modules.Jobs.Core.Application.Common.Contracts;
using FindMyWork.Modules.Jobs.Core.Application.Common.Contracts.Database;
using FindMyWork.Modules.Jobs.Core.Application.Common.Utils;
using FindMyWork.Modules.Jobs.Core.Application.Jobs;
using FindMyWork.Modules.Jobs.Core.Application.Jobs.Contracts;
using FindMyWork.Modules.Jobs.Core.Application.Jobs.Helpers;
using FindMyWork.Modules.Jobs.Core.Application.Jobs.Mappings;
using FindMyWork.Modules.Jobs.Core.Infrastructure.Persistence;
using FindMyWork.Modules.Jobs.Core.Infrastructure.Persistence.Repositories;
using FindMyWork.Shared.Infrastructure.Database.Postgres;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
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
        services.AddScoped<IPaginationHelper, PaginationHelper>();
        
        
        services.AddPostgres<ApplicationDbContext>();

        services.AddFluentValidation(fv =>
        {
            fv.RegisterValidatorsFromAssemblyContaining<ApplicationDbContext>();
            fv.DisableDataAnnotationsValidation = true;
        });
        
        services.AddApiVersioning(options =>
        {
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.ApiVersionReader = new UrlSegmentApiVersionReader();
        });
        
        services.AddHttpContextAccessor();
        services.AddSingleton<IUriService>(o =>
        {
            var accessor = o.GetRequiredService<IHttpContextAccessor>();
            var request = accessor.HttpContext?.Request;
            var uri = string.Concat(request?.Scheme, "://", request?.Host.ToUriComponent());
            
            return new UriService(uri);
        });
        
        return services;
    }
}