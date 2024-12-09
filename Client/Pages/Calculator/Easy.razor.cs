using Microsoft.AspNetCore.Components;
using Shared;
using System.Linq.Expressions;
using System.Net.Http.Json;
using System.Security.Cryptography;


namespace Client.Pages;
public class EasyBase : ComponentBase
{
    [Inject]
    protected HttpClient _httpClient { get; set; }
    protected int number1;
    protected int number2;
    protected string operation;
    protected int correctAnswer;
    protected string userAnswer = string.Empty;
    protected bool showResult = false;
    protected bool isCorrect = false;
    protected int randomNumber;

    protected Random random = new Random();

    protected override void OnInitialized()
    {
        GenerateNewProblem(random.Next(1, 10), random.Next(1,10), random.Next(0, 2));
    }

    protected void GenerateNewProblem(int number1Given, int number2Given, int randomNumberGiven)
    {
        showResult = false;


        number1 = number1Given;
        number2 = number2Given;
        randomNumber = randomNumberGiven;

        if (randomNumber == 0)
        {
            operation = "+";
            correctAnswer = number1 + number2;
        }
        else
        {
            operation = "-";

            if (number1 < number2)
            {
                (number1, number2) = (number2, number1);
            }
            correctAnswer = number1 - number2;
        }

        userAnswer = string.Empty;
    }

    protected async Task CheckAnswer()
    {
        try
        {
            int parsedAnswer;
            try
            {
                parsedAnswer = Int32.Parse(userAnswer);
                isCorrect = parsedAnswer == correctAnswer;
                showResult = true;
            }
            catch (FormatException fe)
            {
                isCorrect = false;
                showResult = true;
                throw new ClientInputException("Input was not in a correct format.", fe);
            }
        }
        catch (ClientInputException cie)
        {
            await sendException(cie);
        }
        showResult = true;

        if (isCorrect)
        {
            await SubmitResult(10);
        }
        else
        {
            await SubmitResult(0);
        }
    }

    protected async Task sendException(ClientInputException e)
    {
        var exceptionDetails = new ExceptionResult
        (
            message : e.Message,
            innerMessage : e.InnerException?.Message,
            stackTrace : e.StackTrace
        );

        await _httpClient.PostAsJsonAsync("api/ExceptionCatcher", exceptionDetails);
    }
    protected async Task SubmitResult(int score)
    {
        var result = new CalcGameResult
        {
            Difficulty = "Easy",
            Score = score
        };

        await _httpClient.PostAsJsonAsync("api/calcgameresults", result);
    }

}