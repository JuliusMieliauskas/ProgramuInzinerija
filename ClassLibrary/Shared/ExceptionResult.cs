using System.Diagnostics;
namespace Shared;
public class ExceptionResult
{
    public int Id { get; set; }
    public String Message { get; set; }
    public String InnerMessage { get; set; }
    public String TheStackTrace { get; set; }
    public DateTime Date { get; set; }
    public ExceptionResult(String message, String innerMessage, String stackTrace)
    {
        Message = message;
        InnerMessage = innerMessage;
        TheStackTrace = stackTrace;
        Date = DateTime.Now;
    }
    public ExceptionResult(){
        Date = DateTime.Now;
    }
}