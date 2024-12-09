using Client.Pages;
namespace Tests;
public class EasyCalculationTests : EasyBase
{
    [Fact]
    public void OnInitialized_GeneratenewRandomProblem()
    {
        OnInitialized();
        Assert.InRange(number1, 1, 10);
        Assert.InRange(number2, 1, 10);
        if (operation == "+")
        {
            Assert.Equal(correctAnswer, number1 + number2);
        }
        if (operation == "-")
        {
            Assert.Equal(correctAnswer, number1 - number2);
        }
        Assert.Equal(userAnswer, string.Empty);
    }

    [Fact]
    public void GenerateNewProblem_Addition()
    {
        GenerateNewProblem(5, 9, 0);
        Assert.Equal(5, number1);
        Assert.Equal(9, number2);
        Assert.InRange(randomNumber, 0, 1);
        Assert.Equal(string.Empty, userAnswer);

        Assert.Equal("+", operation);
        Assert.Equal(correctAnswer, number1 + number2);
    }

    [Fact]
    public void GenerateNewProblem_Subtraction_Number1BiggerThanNumber2()
    {
        GenerateNewProblem(5, 9, 1);
        Assert.Equal(9, number1);
        Assert.Equal(5, number2);
        Assert.InRange(randomNumber, 0, 1);
        Assert.Equal(string.Empty, userAnswer);

        Assert.Equal("-", operation);
        Assert.Equal(correctAnswer, number1 - number2);
    }

    [Fact]
    public void GenerateNewProblem_Subtraction_Number1LesserThanNumber2()
    {
        GenerateNewProblem(9, 5, 1);
        Assert.Equal(9, number1);
        Assert.Equal(5, number2);
        Assert.InRange(randomNumber, 0, 1);
        Assert.Equal(string.Empty, userAnswer);

        Assert.Equal("-", operation);
        Assert.Equal(correctAnswer, number1 - number2);
    }




}
