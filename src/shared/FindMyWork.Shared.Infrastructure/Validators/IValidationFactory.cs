using FindMyWork.Shared.Application.Models.ResponseModels;

namespace FindMyWork.Shared.Infrastructure.Validators;

public interface IValidationFactory
{
    Task<ErrorResponse?> ValidateAsync<TRequest>(TRequest request);
}