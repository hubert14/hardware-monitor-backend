using Microsoft.AspNetCore.Components;

namespace HardwareMon.UI.Components
{
    public partial class PagesButtonsBlock
    {
        [Parameter] public EventCallback TriggerAlert { get; set; }
        [Parameter] public EventCallback TriggerLinks { get; set; }

        [Parameter] public bool CurrentAlertState { get; set; }

        private async Task OnAlertButtonClickAsync()
        {
            await TriggerAlert.InvokeAsync();
        }

        private async Task OnLinksButtonClickAsync()
        {
            await TriggerLinks.InvokeAsync();
        }
    }
}
