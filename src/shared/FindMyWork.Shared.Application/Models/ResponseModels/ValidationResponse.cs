namespace FindMyWork.Shared.Application.Models.ResponseModels;

public record ValidationResponse
{
    public ValidationResponse(string fieldName, string message)
    {
        FieldName = fieldName;
        Message = message;
    }

    public string FieldName { get; init; }
    public string Message { get; init; }
};