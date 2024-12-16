using Microsoft.AspNetCore.Components;
using Shared;
using System.Net.Http.Json;
using System.Threading;

namespace Client.Pages
{
    public class EasyBase : ComponentBase
    {
        [Inject]
        protected HttpClient HttpClient { get; set; } = null!;
        protected int number1;
        protected int number2;
        protected string operation = string.Empty;
        protected int correctAnswer;
        protected string userAnswer = string.Empty;
        protected bool showResult = false;
        protected bool isCorrect = false;
        protected int totalRounds = 0;
        protected int correctAnswers = 0;

        protected int remainingTime = 30;
        protected bool timeIsUp = false;
        private Timer? countdownTimer;
        private Random random = new Random();

        protected override void OnInitialized()
        {
            RestartGame();
        }

        protected void GenerateNewProblem()
        {
            if (timeIsUp) return;

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
            if (timeIsUp) return;

            totalRounds++;

            if (int.TryParse(userAnswer, out int parsedAnswer))
            {
                if (parsedAnswer == correctAnswer)
                {
                    isCorrect = true;
                    correctAnswers++;
                }
                else
                {
                    isCorrect = false;
                }
            }
            else
            {
                isCorrect = false;
            }

            showResult = true;
        }

        protected void StartCountdown()
        {
            countdownTimer = new Timer(OnCountdownElapsed, null, 1000, 1000); // 1-second interval
        }

        private void OnCountdownElapsed(object? state)
        {
            if (remainingTime > 0)
            {
                remainingTime--;
                InvokeAsync(StateHasChanged); // Update UI on the main thread
            }
            else
            {
                timeIsUp = true;
                countdownTimer?.Dispose();
                countdownTimer = null;
                InvokeAsync(StateHasChanged);
            }
        }

        protected void RestartGame()
        {
            remainingTime = 30;
            timeIsUp = false;
            totalRounds = 0;
            correctAnswers = 0;
            StartCountdown();
            GenerateNewProblem();
        }
    }
}
