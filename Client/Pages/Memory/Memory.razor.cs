using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Net.Http.Json;
using System.Net.Http;
using Shared;

namespace Client.Pages;
public class MemoryBase : ComponentBase
{
    protected int missmatches = 0;
    protected string message;
    protected bool gameOver = false;
    public enum Difficulty { Easy, Medium, Hard }
    [Inject]
    protected NavigationManager? NavigationManager { get; set; }

    protected void NavigateToEasy() => NavigationManager.NavigateTo("/easyMem");
    protected void NavigateToMedium() => NavigationManager.NavigateTo("/mediumMem");
    protected void NavigateToHard() => NavigationManager.NavigateTo("/hardMem");

    protected HttpClient? _httpClient { get; set; }
    //protected List<MemoryGameResult> memoryResultList = new List<MemeoryGameResult>();

    protected async Task SaveResults(){
        try
        {
            if (_httpClient == null) throw new ClientNullException();
        }
        catch(ClientNullException){
            message = "Server Error, results cannot be saved!";
            return;
        }
        if (gameOver){
            MemoryGameResult result = new MemoryGameResult(missmatches: missmatches);
            //MemoryResultList.Add(result);

            missmatches = 0;
            await _httpClient.PostAsJsonAsync("api/memorygameresults", result);
        }
    }
}