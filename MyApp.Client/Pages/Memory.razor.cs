using Microsoft.AspNetCore.Components;

namespace MyApp.Client
{
    public class MemoryGame : ComponentBase
    {
        public List<Card> Cards = new();
        public List<int> FlippedCards = new();
    
        protected override void OnInitialized()
        {
            var cardPairs = Enumerable.Range(1, 8).SelectMany(x => new[] { x, x }).ToList();
            Random rng = new Random();
            cardPairs = cardPairs.OrderBy(x => rng.Next()).ToList();
            Cards = cardPairs.Select(x => new Card { Id = x, Number = x, Color = GetRandomColor() }).ToList();
        }
    
        private string GetRandomColor()
        {
            var colors = new[] { "red", "blue", "green", "yellow", "purple", "orange" };
            return colors[Random.Shared.Next(colors.Length)];
        }
    
        public void FlipCard(int cardId)
        {
            if(FlippedCards.Contains(cardId))
            {
                return;
            }
            FlippedCards.Add(cardId);
            if(FlippedCards.Count == 2)
            {
                if(Cards[FlippedCards[0]].Id == Cards[FlippedCards[1]].Id)
                {
                    FlippedCards.Clear();
                }
                else
                {
                    Task.Delay(1000).ContinueWith(_ =>
                    {
                        FlippedCards.Clear();
                        StateHasChanged();
                    });
                }
            }
            StateHasChanged();
        }
        public class Card
        {
            public int Id { get; set; }
            public int Number { get; set; }
            public string Color {get; set;}
        }
    }
}
