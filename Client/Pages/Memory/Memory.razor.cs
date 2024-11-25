using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Net.Http.Json;
using System.Net.Http;
using Shared;
using MyApp.Pages.Calculatoor;

namespace Client.Pages;

public class MemoryBase : ComponentBase
{
    public enum Difficulty { Easy, Medium, Hard }
    Tuple<int, int> gridEasy = new  Tuple<int, int>(4, 4);
    Tuple<int, int> gridMedium = new Tuple<int, int>(4, 6);
    Tuple<int, int> gridHard = new Tuple<int, int>(6, 6);
}

public class Card
{
    private bool isFlippedUp = false;
    private int cardFace = 0;
}