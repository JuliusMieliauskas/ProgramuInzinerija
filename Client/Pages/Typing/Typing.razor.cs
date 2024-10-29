using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Timers;
using Shared;
using System.Net.Http.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace Client.Pages;

public class TypingBase : ComponentBase
    {
        protected string SampleText = "";
        protected string UserInput = "";
        protected System.Timers.Timer GameTimer;
        protected int TimeRemaining = 30;
        protected int WPM = 0;
        protected int ErrorCount = 0;
        protected bool GameStarted = false;
        protected ElementReference textAreaReference;
        
        [Inject]
        private HttpClient _httpClient { get; set; }

        [Inject]
        private ILogger<TypingBase> Logger { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                Logger.LogInformation("Fetching sample text from server.");
                SampleText = await _httpClient.GetStringAsync("api/sampletext/sample-text");
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Failed to fetch sample text.");
                SampleText = "Error loading text.";
            }

            GameTimer = new System.Timers.Timer(1000);
            GameTimer.Elapsed += (sender, e) => 
            {
                TimeRemaining--;
                if (TimeRemaining <= 0)
                {
                    EndGame();
                }
                CalculateWPM();
                InvokeAsync(StateHasChanged);
            };
        }

        protected async Task StartGame()
        {
            UserInput = "";
            TimeRemaining = 10;
            WPM = 0;
            ErrorCount = 0;
            GameStarted = true;
            GameTimer.Start();

            await Task.Delay(10);
            await textAreaReference.FocusAsync();
        }

        protected void EndGame()
        {
            GameTimer.Stop();
            GameStarted = false;
            CalculateWPM();
            SaveTypingGameResultsAsync();
        }

        protected void HandleKeyDown(KeyboardEventArgs e)
        {
            if (!GameStarted)
            {
                return;
            }

            CalculateWPM();
        }

        protected void CalculateWPM()
        {
            int totalCharacters = UserInput.Length;
            double minutes = (30 - TimeRemaining) / 60.0;
            
            if (minutes > 0)
            {
                WPM = (int)(totalCharacters / 5.0 / minutes);
            }

            ErrorCount = UserInput
                .Take(Math.Min(UserInput.Length, SampleText.Length))
                .Where((c, i) => c != SampleText[i])
                .Count();
        }

        private async Task SaveTypingGameResultsAsync()
        {
            var result = new TypingGameResult
            {
                WordsPerMinute = WPM,
                Errors = ErrorCount,
                Status = (ErrorCount == 0 ? TypingGameStatus.PerfectRun : TypingGameStatus.NotPerfectRun)
            };
            
            Logger.LogInformation("Saving typing game results: {WPM} WPM, {Errors} errors", WPM, ErrorCount);

            await _httpClient.PostAsJsonAsync("api/typinggameresults", result);
        }
    }