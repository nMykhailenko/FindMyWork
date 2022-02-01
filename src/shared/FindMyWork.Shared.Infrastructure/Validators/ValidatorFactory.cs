using OneOf;
using System.Text.Json;
using FindMyWork.Shared.Application.Models.ErrorModels;
using FindMyWork.Shared.Application.Models.ResponseModels;
using FindMyWork.Shared.Application.Models.SuccessModels;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace FindMyWork.Shared.Infrastructure.Validators;

public class ValidatorFactory: IValidationFactory
{
    private readonly IServiceProvider _serviceProvider;


    public ValidatorFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<OneOf<ValidationSuccess, EntityNotValid>> ValidateAsync<TRequest>(TRequest request)
    {
        var validator = _serviceProvider.GetRequiredService<IValidator<TRequest>>();
        
        var validationResult = await validator.ValidateAsync(request);
        if (validationResult.IsValid)
        {
            return new ValidationSuccess();
        }
        
        var errors = validationResult.Errors
            .GroupBy(x => x.PropertyName)
            .ToDictionary(
                g => g.Key,
                g => g.Select(x => x.ErrorMessage).ToArray()
            )
            .Select(x => new ValidationResponse(x.Key, string.Join(",", x.Value)));

        return new EntityNotValid(JsonSerializer.Serialize(errors));
    }
}