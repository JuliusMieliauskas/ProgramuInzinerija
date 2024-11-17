using Client.Pages;
namespace Tests;
public class ReactionGameTests
{
    private readonly ReactionBase _reactionClass = new ReactionBase();

    [Fact]
    public void TestStart_SetsInitialValuesCorrectly()
    {
        _reactionClass.TestStart();

        Assert.True(_reactionClass.inAction);
        Assert.Equal("Wait for the button.", _reactionClass.commencingText);
        Assert.InRange(_reactionClass.reactionSpringUp, 2, 4);
    }
    [Fact]
    public async Task TestStart_TriggersSpringUpAfterElapsedTime()
    {
        _reactionClass.TestStart();

        double elapsedTime = 0;
        while (elapsedTime < _reactionClass.reactionSpringUp)
        {
            await Task.Delay(50);
            elapsedTime += 0.05;
        }

        Assert.True(_reactionClass.springUp);
        Assert.Equal("Press!", _reactionClass.commencingText);
    }
    [Fact]
    public void TestEarly_SetsCommencingTextAndResetsTimePassed()
    {
        _reactionClass.TestEarly();

        Assert.Equal("Too early! Wait for the button.", _reactionClass.commencingText);
        Assert.Equal(0, _reactionClass.timePassed);
    }
    [Fact]
    public void TestFinish_DisposesAndReinitializesTimer_SetsFieldsCorrectly()
    {
        _reactionClass.reactionSpringUp = 2.5; 
        _reactionClass.timePassed = 5.0;     

        var originalTimer = _reactionClass.reactionTimer;

        _reactionClass.TestFinish();

        Assert.Throws<ObjectDisposedException>(() => originalTimer.Start());

        Assert.NotNull(_reactionClass.reactionTimer);
        Assert.Equal(50, _reactionClass.reactionTimer.Interval);

        Assert.Equal(2.5, _reactionClass.reactionTime); 

        Assert.Equal(0, _reactionClass.timePassed);

        Assert.False(_reactionClass.springUp);

        Assert.False(_reactionClass.inAction);
    }
    [Fact (Skip = "not implemented")]
    public void reaction_testName()
    {
        throw new NotImplementedException();
    }
}