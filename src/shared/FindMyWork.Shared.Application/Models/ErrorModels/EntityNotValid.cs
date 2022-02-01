namespace FindMyWork.Shared.Application.Models.ErrorModels;

public struct EntityNotValid
{
    public EntityNotValid(string message)
    {
        Message = message;
    }
    
    /// <summary>
    /// Gets or sets an error message.
    /// </summary>
    public string Message;
}