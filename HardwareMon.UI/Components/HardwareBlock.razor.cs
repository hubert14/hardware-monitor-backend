using HardwareMon.Models;
using HardwareMon.UI.Services;
using Microsoft.AspNetCore.Components;

namespace HardwareMon.UI.Components
{
    public partial class HardwareBlock
    {
        [Inject] private HardwareService HardwareService { get; set; }

        private HardwareInfoViewModel _hardware = new(null);

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                if (HardwareService is not null)
                {
                    HardwareService.NewDataArrivedEvent += OnNewDataArrivedAsync;
                    HardwareService.StartDataRecieving();
                }
            }

            base.OnAfterRender(firstRender);
        }

        private async Task OnNewDataArrivedAsync(object sender, CollectingData data)
        {
            _hardware = new HardwareInfoViewModel(data);
            await InvokeAsync(StateHasChanged);
        }
    }
}
