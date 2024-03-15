using HardwareMon.UI.Services;
using Microsoft.AspNetCore.Components;

namespace HardwareMon.UI.Components
{
    public partial class HomeAssistantBlock
    {
        [Inject] private HomeAssistantService HomeAssistantService { get; set; }

        [Parameter] public EventCallback<bool> AlertChanged { get; set; }

        private HomeAssistantDataViewModel _homeAssistant = new();

        private bool _alertState;

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                if (HomeAssistantService is not null)
                {
                    HomeAssistantService.NewDataArrivedEvent += OnNewDataArrivedAsync;
                    HomeAssistantService.StartDataRecieving();
                }
            }

            base.OnAfterRender(firstRender);
        }

        private async Task OnNewDataArrivedAsync(object sender, HomeAssistantDataViewModel data)
        {
            _homeAssistant = data;
            if(data.Alert != _alertState)
            {
                _alertState = data.Alert;
                await AlertChanged.InvokeAsync(data.Alert);
            }

            await InvokeAsync(StateHasChanged);
        }
    }
}
