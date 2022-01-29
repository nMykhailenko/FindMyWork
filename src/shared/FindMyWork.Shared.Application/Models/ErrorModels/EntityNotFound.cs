namespace FindMyWork.Shared.Application.Models.ErrorModels;

public struct EntityNotFound
{
    public EntityNotFound(string message)
    {
        Message = message;
    }
    
    /// <summary>
    /// Gets or sets an error message.
    /// </summary>
    public string Message;
}