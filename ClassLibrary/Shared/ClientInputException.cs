namespace Shared;
public class ClientInputException : FormatException
{
    public int Id { get; set; }
    public ClientInputException() : base() { }
    public ClientInputException(string message) : base(message) { }
    public ClientInputException(string message, Exception innerException) 
        : base(message, innerException) { }

    // Required for serialization (optional but recommended for certain environments)
    protected ClientInputException(System.Runtime.Serialization.SerializationInfo info,
                                   System.Runtime.Serialization.StreamingContext context)
        : base(info, context) { }
}