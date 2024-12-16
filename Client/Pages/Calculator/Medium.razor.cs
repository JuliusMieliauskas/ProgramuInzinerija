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
        protected string errorMessage = string.Empty;

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

            int operationChoice = random.Next(0, 4);

            switch (operationChoice)
            {
                case 0: // Addition
                    number1 = random.Next(1, 10);
                    number2 = random.Next(1, 10);
                    operation = "+";
                    correctAnswer = number1 + number2;
                    break;

                case 1: // Subtraction
                    number1 = random.Next(1, 10);
                    number2 = random.Next(1, 10);

                    if (number1 < number2)
                    {
                        (number1, number2) = (number2, number1);
                    }
                    operation = "-";
                    correctAnswer = number1 - number2;
                    break;

                case 2: // Multiplication
                    number1 = random.Next(1, 10);
                    number2 = random.Next(1, 10);
                    operation = "*";
                    correctAnswer = number1 * number2;
                    break;

                case 3: // Division
                    number2 = random.Next(1, 10);
                    correctAnswer = random.Next(1, 6);
                    number1 = number2 * correctAnswer;
                    operation = "/";
                    break;
            }

            userAnswer = string.Empty;
            errorMessage = string.Empty;
        }

        protected void CheckAnswer()
        {
            if (timeIsUp) return;

            totalRounds++;

            try
            {
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
                    throw new InvalidInputException($"Invalid input: '{userAnswer}' is not a number.");
                }
            }
            catch (InvalidInputException ex)
            {
                LogException(ex);
                errorMessage = ex.Message;
                isCorrect = false;
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

        protected void StartCountdown()
        {
            countdownTimer = new Timer(OnCountdownElapsed, null, 1000, 1000);
        }

        private void OnCountdownElapsed(object? state)
        {
            if (remainingTime > 0)
            {
                remainingTime--;
                InvokeAsync(StateHasChanged);
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

        protected async void SaveResults()
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