using OneOf;
using FindMyWork.Shared.Application.Models.ErrorModels;
using FindMyWork.Shared.Application.Models.SuccessModels;

namespace FindMyWork.Shared.Infrastructure.Validators;

public interface IValidationFactory
{
    Task<OneOf<ValidationSuccess, EntityNotValid>> ValidateAsync<TRequest>(TRequest request);
}