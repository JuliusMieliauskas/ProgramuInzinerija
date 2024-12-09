using Client.Pages;
namespace Tests;
public class ReactionGameTests : ReactionBase
{
    private readonly ReactionBase _reactionClass = new ReactionBase();

    [Fact]
    public void TestStart_SetsInitialValuesCorrectly()
    {
        TestStart();

        Assert.True(inAction);
        Assert.Equal("Wait for the button.", commencingText);
        Assert.InRange(reactionSpringUp, 2, 4);
    }
    [Fact]
    public async Task TestStart_TriggersSpringUpAfterElapsedTime()
    {
        TestStart();

        double elapsedTime = 0;
        while (elapsedTime < reactionSpringUp)
        {
            await Task.Delay(50);
            elapsedTime += 0.05;
        }

        Assert.True(springUp);
        Assert.Equal("Press!", commencingText);
    }
    [Fact]
    public void TestEarly_SetsCommencingTextAndResetsTimePassed()
    {
        TestEarly();

        Assert.Equal("Too early! Wait for the button.", commencingText);
        Assert.Equal(0, timePassed);
    }
    [Fact]
    public void TestFinish_DisposesAndReinitializesTimer_SetsFieldsCorrectly()
    {
        reactionSpringUp = 2.5; 
        timePassed = 5.0;     

        var originalTimer = reactionTimer;

        TestFinish();

        Assert.Throws<ObjectDisposedException>(() => originalTimer.Start());

        Assert.NotNull(reactionTimer);
        Assert.Equal(50, reactionTimer.Interval);

        Assert.Equal(2.5, reactionTime); 

        Assert.Equal(0, timePassed);

        Assert.False(springUp);

        Assert.False(inAction);
    }
}
