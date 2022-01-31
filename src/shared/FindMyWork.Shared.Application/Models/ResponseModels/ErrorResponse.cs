namespace FindMyWork.Shared.Application.Models.ResponseModels;

public record ErrorResponse
{
    public ErrorResponse(string code, string message)
    {
        Code = code;
        Message = message;
    }

    public string Code { get; init; }
    public string Message { get; init; }
};