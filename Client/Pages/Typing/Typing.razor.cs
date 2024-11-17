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
        public string SampleText = "";
        public string UserInput = "";
        public System.Timers.Timer GameTimer;
        public int TimeRemaining = 30;
        public int WPM = 0;
        public int ErrorCount = 0;
        public bool GameStarted = false;
        public ElementReference textAreaReference;
        
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

        public async Task StartGame()
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

        public void EndGame()
        {
            GameTimer.Stop();
            GameStarted = false;
            CalculateWPM();
            SaveTypingGameResultsAsync();
        }

        public void HandleKeyDown(KeyboardEventArgs e)
        {
            if (!GameStarted)
            {
                return;
            }

            CalculateWPM();
        }

        public void CalculateWPM()
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

        public async Task SaveTypingGameResultsAsync()
        {
            var result = new TypingGameResult
            {
                WordsPerMinute = WPM,
                Errors = ErrorCount,
                Status = (ErrorCount == 0 ? TypingGameStatus.PerfectRun : TypingGameStatus.NotPerfectRun)
            };
            
            var result2 = new TypingGameResult(wordsPerMinute: WPM, errors: ErrorCount);
            Logger.LogInformation("Saving typing game results: {WPM} WPM, {Errors} errors", WPM, ErrorCount);

            await _httpClient.PostAsJsonAsync("api/typinggameresults", result2);
        }
    }