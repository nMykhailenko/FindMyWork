namespace FindMyWork.Shared.Application.Models.ResponseModels;

public record ErrorResponseModel
{
    public ErrorResponseModel(string code, string message)
    {
        Code = code;
        Message = message;
    }

    public string Code { get; init; }
    public string Message { get; init; }
};