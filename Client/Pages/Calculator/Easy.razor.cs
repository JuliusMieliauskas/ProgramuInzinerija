using Microsoft.AspNetCore.Components;
using Shared;
using System.Net.Http.Json;
using System.Threading;

namespace Client.Pages
{
    public class EasyBase : ComponentBase
    {
        [Inject]
        public HttpClient HttpClient { get; set; } = null!;
        public int number1 { get; set; }
        public int number2 { get; set; }
        public string operation { get; set; } = string.Empty;
        public int correctAnswer { get; set; }
        public string userAnswer { get; set; } = string.Empty;
        public bool showResult { get; set; } = false;
        public bool isCorrect { get; set; } = false;
        public int totalRounds { get; set; } = 0;
        public int correctAnswers { get; set; } = 0;
        public bool IsTestMode { get; set; } = false;
        public int remainingTime { get; set; } = 30;
        public bool timeIsUp { get; set; } = false;
        private Timer? countdownTimer;
        private Random random = new Random();

        protected override void OnInitialized()
        {
            RestartGame();
        }

        public void GenerateNewProblem()
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

        public void CheckAnswer()
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

        public void StartCountdown()
        {
            countdownTimer = new Timer(OnCountdownElapsed, null, 1000, 1000);
        }
        public void ForceCountdownToExpire()
        {
            while (remainingTime > 0)
            {
                OnCountdownElapsed(null);
            }

            countdownTimer?.Dispose();
            countdownTimer = null;
            timeIsUp = true;
        }

        private void OnCountdownElapsed(object? state)
        {
            if (remainingTime > 0)
            {
                remainingTime--;

                if (!IsTestMode)
                {
                    InvokeAsync(StateHasChanged);
                }
            }
            else
            {
                timeIsUp = true;
                countdownTimer?.Dispose();
                countdownTimer = null;

                if (!IsTestMode)
                {
                    InvokeAsync(StateHasChanged);
                }
            }
        }

        public void RestartGame()
        {
            remainingTime = 30;
            timeIsUp = false;
            totalRounds = 0;
            correctAnswers = 0;
            StartCountdown();
            GenerateNewProblem();
        }

        public async void SaveResults()
        {
            var result = new CalcGameResult
            {
                Difficulty = "Easy",
                CorrectAnswers = correctAnswers,
                TotalRounds = totalRounds,
            };

            try
            {
                await HttpClient.PostAsJsonAsync("api/calcgameresults", result);
                Console.WriteLine("Result saved successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving result: {ex.Message}");
            }
        }
    }
}