using Client.Pages;
namespace Tests;
public class EasyCalculationTests : EasyBase
{
    [Fact]
    public void GenerateNewProblem_RandomGenerate()
    {
        GenerateNewProblem();
        Assert.InRange(number1, 1, 10);
        Assert.InRange(number2, 1, 10);
        if (operation == "+")
        {
            Assert.Equal(correctAnswer, number1 + number2);
        }
        else
        {
            if (number1 < number2)
            {
                (number1, number2) = (number2, number1);
            }
            Assert.Equal(correctAnswer, number1 - number2);
        }
        Assert.Equal(userAnswer, string.Empty);
    }

    [Fact]
    public async Task CheckAnswer_ValidInput_SetsIsCorrectAndSubmitsResult()
    {
        userAnswer = "6";
        correctAnswer = 6;

        CheckAnswer();
        await Task.Delay(100); // Wait for async void to complete

        // Assert
        Assert.True(isCorrect);
        Assert.True(showResult);
    }
}
