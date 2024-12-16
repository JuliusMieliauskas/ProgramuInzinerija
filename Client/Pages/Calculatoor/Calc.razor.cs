using Microsoft.AspNetCore.Components;

namespace Client.Pages
{
    public class CalcBase : ComponentBase
    {
        [Inject]
        protected NavigationManager NavigationManager { get; set; } = null!;
        protected void NavigateToEasy() => NavigationManager.NavigateTo("/easy");
    }
}