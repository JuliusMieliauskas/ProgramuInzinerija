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
        protected int totalRounds = 0;
        protected int correctAnswers = 0;

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
            totalRounds++;

            if (int.TryParse(userAnswer, out int parsedAnswer))
            {
                isCorrect = parsedAnswer == correctAnswer;
            }
            else
            {
                isCorrect = false;
            }

            showResult = true;

            if (totalRounds >= 10)
            {
                await SubmitResult();
            }

        }

        protected async Task SubmitResult()
        {
            var result = new CalcGameResult
            {
                Difficulty = "Easy",
                CorrectAnswers = correctAnswers,
                TotalRounds = totalRounds,
            };

            await HttpClient.PostAsJsonAsync("api/calcgameresults", result);

            totalRounds = 0;
            correctAnswers = 0;
            GenerateNewProblem();
        }

    }
}