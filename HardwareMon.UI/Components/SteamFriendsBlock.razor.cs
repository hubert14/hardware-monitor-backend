using HardwareMon.UI.Services;
using Microsoft.AspNetCore.Components;

namespace HardwareMon.UI.Components
{
    public partial class SteamFriendsBlock : IDisposable
    {
        [Inject] private SteamService SteamService { get; set; }

        private SteamDataViewModel _steamProfiles = new();

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                if (SteamService is not null)
                {
                    SteamService.NewDataArrivedEvent += OnNewDataArrivedAsync;
                    SteamService.StartDataRecieving();
                }
            }

            base.OnAfterRender(firstRender);
        }

        private async Task OnNewDataArrivedAsync(object sender, SteamDataViewModel data)
        {
            _steamProfiles = data;
            await InvokeAsync(StateHasChanged);
        }

        private bool _disposedValue;
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    SteamService.NewDataArrivedEvent -= OnNewDataArrivedAsync;
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