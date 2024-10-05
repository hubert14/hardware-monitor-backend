using Microsoft.AspNetCore.Components;

namespace HardwareMon.UI.Components
{
    public partial class ButtonsBlock
    {
        [Parameter] public EventCallback TriggerAlert { get; set; }
        [Parameter] public EventCallback TriggerLinks { get; set; }
        [Parameter] public EventCallback OnPreviousClick { get; set; }
        [Parameter] public EventCallback OnNextClick { get; set; }

        [Parameter] public bool CurrentAlertState { get; set; }

        private async Task OnAlertButtonClickAsync()
        {
            await TriggerAlert.InvokeAsync();
        }

        private async Task OnLinksButtonClickAsync()
        {
            await TriggerLinks.InvokeAsync();
        }

        private async Task OnPreviousButtonClickAsync()
        {
            await OnPreviousClick.InvokeAsync();
        }

        private async Task OnNextButtonClickAsync()
        {
            await OnNextClick.InvokeAsync();
        }

    }
}
