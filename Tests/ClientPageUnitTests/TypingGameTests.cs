using Client.Pages;
namespace Tests;
public class TypingGameTests
{
    private readonly TypingBase _typingClass = new TypingBase();

    // [Theory]
    // [InlineData("hello world", "hello world", 20, 12, 0)]   
    // [InlineData("hello world", "hella warld", 20, 12, 2)]   
    // [InlineData("helloworld", "hello world", 15, 16, 1)]    
    // [InlineData("quick brown fox", "quick brown dog", 10, 24, 3)] 
    // [InlineData("", "hello world", 25, 0, 0)]               
    // [InlineData("test", "testing", 0, 0, 3)]               
    // public void CalculateWPM_CalculatesCorrectWPMAndErrorCount(string userInput, string sampleText, int timeRemaining, int expectedWPM, int expectedErrorCount)
    // {
    //     _typingClass.UserInput = userInput;
    //     _typingClass.SampleText = sampleText;
    //     _typingClass.TimeRemaining = timeRemaining;
    //     _typingClass.WPM = 0; 
    //     _typingClass.ErrorCount = 0; 

    //     _typingClass.CalculateWPM();

    //     Assert.Equal(expectedWPM, _typingClass.WPM);
    //     Assert.Equal(expectedErrorCount, _typingClass.ErrorCount);
    // }
}