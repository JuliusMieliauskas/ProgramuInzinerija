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
    [Inject]
    protected NavigationManager? NavigationManager { get; set; }

    protected void NavigateToEasy() => NavigationManager.NavigateTo("/easyMem");
    protected void NavigateToMedium() => NavigationManager.NavigateTo("/mediumMem");
    protected void NavigateToHard() => NavigationManager.NavigateTo("/hardMem");
}

public class Card
{
    private bool isFlippedUp = false;
    private int cardFace = 0;
}