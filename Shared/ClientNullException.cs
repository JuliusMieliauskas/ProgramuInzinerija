namespace Shared;
public class ClientNullException : Exception
{
    public string? ExceptionLocation { get; }

    public ClientNullException() { }

    public ClientNullException(string message)
        : base(message) { }
    public ClientNullException(string message, string exceptionLocation)
        : this(message)
    {
        ExceptionLocation = exceptionLocation;
    }

    public ClientNullException(string message, Exception inner)
        : base(message, inner) { }
}