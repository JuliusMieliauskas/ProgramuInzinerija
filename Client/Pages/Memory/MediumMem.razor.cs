using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Net.Http.Json;
using System.Net.Http;
using Shared;

namespace Client.Pages;
public class MediumMemBase : ComponentBase
{
    protected List<List<Card>> Cards { get; set; }
    protected List<Card> flippedCards = new List<Card>();
    protected int wrongs = 0;
    protected int victories = 0;
    protected bool flipping = false;
    protected override void OnInitialized()
    {
        startGame();
    }

    public void startGame()
    {
        var uniqueCards = new List<Card>();
        uniqueCards.Add(new Card() { number = 1, color = "red" });
        uniqueCards.Add(new Card() { number = 2, color = "orange" });
        uniqueCards.Add(new Card() { number = 3, color = "yellow" });
        uniqueCards.Add(new Card() { number = 4, color = "lime" });
        uniqueCards.Add(new Card() { number = 5, color = "green" });
        uniqueCards.Add(new Card() { number = 6, color = "cyan" });
        uniqueCards.Add(new Card() { number = 7, color = "aqua" });
        uniqueCards.Add(new Card() { number = 8, color = "blue" });
        uniqueCards.Add(new Card() { number = 9, color = "purple"});
        uniqueCards.Add(new Card() { number = 10, color = "magenta"});
        uniqueCards.Add(new Card() { number = 11, color = "pink"});
        uniqueCards.Add(new Card() { number = 12, color = "rose"});
        uniqueCards.AddRange(uniqueCards.Select(c => new Card() { number = c.number, color = c.color }).ToList());
        var cardPairs = uniqueCards.OrderBy(x => Guid.NewGuid()).ToList();

        Cards = new List<List<Card>>();
        for (int i = 0; i < 16; i++)
        {
            if (i % 4 == 0)
            {
                Cards.Add(new List<Card>());
            }
            Cards[i / 4].Add(cardPairs[i]);
        }
    }
    public async Task flip(Card card)
    {
        if (flipping) return;
        flipping = true;
        card.flipped = true;
        await Task.Delay(100);
        flippedCards.Add(card);
        if (flippedCards.Count() % 2 == 0)
        {
            var lastTwo = flippedCards.TakeLast(2);
            var last = lastTwo.Last();
            var secondLast = lastTwo.First();
            if (last.number != secondLast.number)
            {
                await Task.Delay(600);
                wrongs++;
                last.flipped = false;
                secondLast.flipped = false;
                flippedCards.Remove(last);
                flippedCards.Remove(secondLast);
            }
        }
        if (flippedCards.Count() == 16)
        {
            flippedCards.Clear();
            await Task.Delay(600);
            wrongs = 0;
            victories++;
            startGame();
        }
        flipping = false;
    }
    public class Card
    {
        public string text { get { return flipped ? number.ToString() : string.Empty; } }
        public int number { get; set; }
        public string color { get; set; }
        public bool flipped { get; set; }
    }
}