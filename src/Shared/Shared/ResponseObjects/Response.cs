namespace Shared.ResponseObjects;

public class Response<T>
{
    public Response()
    {
    }
    public Response(T data, string message)
    {
        Succeeded = true;
        Message = message;
        Data = data;
    }
    public Response(string message)
    {
        Succeeded = true;
        Message = message;
    }

    public Response(string message, bool succeeded)
    {
        Succeeded = succeeded;
        Message = message;
    }
    public bool Succeeded { get; set; }
    public string Message { get; set; } = string.Empty;
    public List<string> Errors { get; set; } = new List<string>();
    public T? Data { get; set; }
}

