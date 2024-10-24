using HardwareMon.Models;
using HardwareMon.UI.Services;
using Microsoft.AspNetCore.Components;

namespace HardwareMon.UI.Components.Pages
{
    public partial class HardwarePage : IDisposable
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

        private bool _disposedValue;
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    HardwareService.NewDataArrivedEvent -= OnNewDataArrivedAsync;
                }

                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
