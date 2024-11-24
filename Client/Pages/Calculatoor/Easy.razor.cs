using Microsoft.AspNetCore.Components;
using Shared;
using System.Net.Http.Json;


namespace Client.Pages
{
    public class EasyBase : ComponentBase
    {
        [Inject]
        protected HttpClient HttpClient { get; set; }
        protected int number1;
        protected int number2;
        protected string operation;
        protected int correctAnswer;
        protected string userAnswer = string.Empty;
        protected bool showResult = false;
        protected bool isCorrect = false;

        private Random random = new Random();

        protected override void OnInitialized()
        {
            GenerateNewProblem();
        }

        protected void GenerateNewProblem()
        {
            showResult = false;

            number1 = random.Next(1, 10);
            number2 = random.Next(1, 10);

            if (random.Next(0, 2) == 0)
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

        protected async void CheckAnswer()
        {
            if (int.TryParse(userAnswer, out int parsedAnswer))
            {
                isCorrect = parsedAnswer == correctAnswer;
            }
            else
            {
                isCorrect = false;
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

        protected async Task SubmitResult(int score)
        {
            var result = new CalcGameResult
            {
                Difficulty = "Easy",
                Score = score,
                Date = DateTime.Now
            };

            await HttpClient.PostAsJsonAsync("api/calcgameresults", result);
        }

    }
}