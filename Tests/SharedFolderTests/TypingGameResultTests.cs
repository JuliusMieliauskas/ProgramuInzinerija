using Client.Pages;
using Shared;
namespace Tests;
public class TypingGameResultTests
{


    [Fact]
    public void TypingGameResult_ConstructorTest_PerfectRun()
    {
        TypingGameResult mockClass = new TypingGameResult
        (
            wordsPerMinute: 103,
            errors: 0,
            id: 3
        );
        Assert.Equal(0, mockClass.Errors);
        Assert.Equal(3, mockClass.Id);
        Assert.Equal(TypingGameStatus.PerfectRun, mockClass.Status);
        Assert.Equal(103, mockClass.WordsPerMinute);
        Assert.Equal(103, mockClass.WpmToErrorRatio);
    }
    [Fact]
    public void TypingGameResult_ConstructorTest_NonPerfectRun()
    {
        TypingGameResult mockClass = new TypingGameResult
        (
            wordsPerMinute: 100,
            errors: 5,
            id: 5
        );
        Assert.Equal(5, mockClass.Errors);
        Assert.Equal(5, mockClass.Id);
        Assert.Equal(TypingGameStatus.NotPerfectRun, mockClass.Status);
        Assert.Equal(100, mockClass.WordsPerMinute);
        Assert.Equal(20, mockClass.WpmToErrorRatio);
    }

    // public int CompareTo(TypingGameResult? other)
    // {
    //     if (other == null) return 1;

    //     return WpmToErrorRatio.CompareTo(other.WpmToErrorRatio);
    // }

    [Fact]
    public void TypingGameResult_CompareTo_Smaller()
    {
        // Mock 20 WPM/Error class
        TypingGameResult mockClassSmall = new TypingGameResult(wordsPerMinute: 100, errors: 5);
        // Mock 100 WPM/Error class
        TypingGameResult mockClassBig = new TypingGameResult(wordsPerMinute: 100, errors: 0);

        Assert.Equal(1, mockClassBig.CompareTo(mockClassSmall));
    }
    [Fact]
    public void TypingGameResult_CompareTo_Bigger()
    {
        // Mock 20 WPM/Error class
        TypingGameResult mockClassSmall = new TypingGameResult(wordsPerMinute: 100, errors: 5);
        // Mock 100 WPM/Error class
        TypingGameResult mockClassBig = new TypingGameResult(wordsPerMinute: 100, errors: 0);

        Assert.Equal(-1, mockClassSmall.CompareTo(mockClassBig));
    }
    [Fact]
    public void TypingGameResult_CompareTo_Equal()
    {
        // Mock 100 WPM/Error class
        TypingGameResult mockClassSmall = new TypingGameResult(wordsPerMinute: 100, errors: 0);
        // Mock 100 WPM/Error class
        TypingGameResult mockClassBig = new TypingGameResult(wordsPerMinute: 100, errors: 0);

        Assert.Equal(0, mockClassBig.CompareTo(mockClassSmall));
    }
}
