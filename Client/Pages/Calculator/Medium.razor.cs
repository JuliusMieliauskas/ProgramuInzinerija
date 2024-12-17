using Microsoft.AspNetCore.Components;
using Shared;
using System.Net.Http.Json;
using System.IO;
using System.Threading;

namespace Client.Pages
{
    public class MediumBase : ComponentBase
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
        public string errorMessage { get; set; } = string.Empty;

        public int remainingTime { get; set; } = 30;
        public bool timeIsUp { get; set; } = false;
        private Timer? countdownTimer;
        public bool IsTestMode { get; set; } = false;
        private Random random = new Random();

        protected override void OnInitialized()
        {
            RestartGame();
        }

        public void GenerateNewProblem()
        {
            if (timeIsUp) return;

            showResult = false;

            int operationChoice = random.Next(0, 4);

            switch (operationChoice)
            {
                case 0:
                    number1 = random.Next(1, 10);
                    number2 = random.Next(1, 10);
                    operation = "+";
                    correctAnswer = number1 + number2;
                    break;

                case 1:
                    number1 = random.Next(1, 10);
                    number2 = random.Next(1, 10);

                    if (number1 < number2)
                    {
                        (number1, number2) = (number2, number1);
                    }
                    operation = "-";
                    correctAnswer = number1 - number2;
                    break;

                case 2:
                    number1 = random.Next(1, 10);
                    number2 = random.Next(1, 10);
                    operation = "*";
                    correctAnswer = number1 * number2;
                    break;

                case 3:
                    number2 = random.Next(1, 10);
                    correctAnswer = random.Next(1, 6);
                    number1 = number2 * correctAnswer;
                    operation = "/";
                    break;
            }

            userAnswer = string.Empty;
            errorMessage = string.Empty;
        }

        public void CheckAnswer()
        {
            if (timeIsUp) return;

            totalRounds++;

            if (int.TryParse(userAnswer, out int parsedAnswer))
            {
                isCorrect = parsedAnswer == correctAnswer;
                if (isCorrect)
                {
                    correctAnswers++;
                }
                errorMessage = string.Empty;
            }
            else
            {
                errorMessage = $"Invalid input: '{userAnswer}' is not a number.";
                isCorrect = false;

                if (IsTestMode)
                {
                    LogException(new InvalidInputException(errorMessage));
                }
            }

            showResult = true;
        }

        private void LogException(Exception ex)
        {
            try
            {
                string logDirectory = Path.Combine("wwwroot", "logs");
                string logFilePath = Path.Combine(logDirectory, "exceptions.log");

                if (!Directory.Exists(logDirectory))
                {
                    Directory.CreateDirectory(logDirectory);
                }

                string logMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {ex.Message}\n";
                File.AppendAllText(logFilePath, logMessage);
            }
            catch (Exception fileEx)
            {
                Console.WriteLine($"Failed to log exception: {fileEx.Message}");
            }
        }

        public void StartCountdown(Timer? testTimer = null)
        {
            countdownTimer = testTimer ?? new Timer(OnCountdownElapsed, null, 1000, 1000);
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
                Difficulty = "Medium",
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