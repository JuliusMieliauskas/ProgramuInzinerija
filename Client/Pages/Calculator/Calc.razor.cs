using Microsoft.AspNetCore.Components;

namespace Client.Pages
{
    public class CalcBase : ComponentBase
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; } = null!;

        public void NavigateToEasy() => NavigationManager.NavigateTo("/easy");
        public void NavigateToMedium() => NavigationManager.NavigateTo("/medium");
    }
}
