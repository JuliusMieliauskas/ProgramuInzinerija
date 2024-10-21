using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Timers;
using MyApp.Shared;
using System.Net.Http.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyApp.Pages;

public class TypingBase : ComponentBase
    {
        protected string SampleText = "Breakfast procuring nay end happiness allowance assurance frankness. Met simplicity nor difficulty unreserved who. Entreaties mr conviction dissimilar me astonished estimating cultivated. On no applauded exquisite my additions. Pronounce add boy estimable nay suspected. You sudden nay elinor thirty esteem temper. Quiet leave shy you gay off asked large style. Oh to talking improve produce in limited offices fifteen an. Wicket branch to answer do we. Place are decay men hours tiled. If or of ye throwing friendly required. Marianne interest in exertion as. Offering my branched confined oh dashwood.";
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

        protected override void OnInitialized()
        {
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
                Errors = ErrorCount
            };
            Logger.LogInformation("Saving typing game results: {WPM} WPM, {Errors} errors", WPM, ErrorCount);

            await _httpClient.PostAsJsonAsync("api/typinggameresults", result);
        }
    }