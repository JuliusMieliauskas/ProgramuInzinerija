using Client.Pages;
using Shared;
namespace Tests;

public class ClientInputExceptionTests
{
    [Fact]
    public void ClientInputException_Constructor_NoArguments()
    {
        var mockClientInputException = new ClientInputException();
        Assert.Equal(0, mockClientInputException.Id);
    }

    
}
