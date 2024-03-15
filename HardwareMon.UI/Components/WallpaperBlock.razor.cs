using HardwareMon.UI.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace HardwareMon.UI.Components
{
    public partial class WallpaperBlock
    {
        [Inject] private WallpaperService WallpaperService { get; set; }
        [Inject] private IJSRuntime JSRuntime { get; set; }

        private string _currentImageUrl;

        private bool _isVideo;

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                if (WallpaperService is not null)
                {
                    WallpaperService.ImageChangedEvent += (async (s, a) => await OnNewDataArrived(s,a));
                }
            }

            base.OnAfterRender(firstRender);
        }

        public void SetPrevious()
        {
            WallpaperService.CurrentImageIndex--;
        }

        public void SetNext()
        {
            WallpaperService.CurrentImageIndex++;
        }

        private async Task OnNewDataArrived(object sender, string newImage)
        {
            _isVideo = newImage.EndsWith(".mp4");
            await LoadMediaAsync(newImage);
            if(_isVideo) await JSRuntime.InvokeVoidAsync("muteVideo");
        }

        private async Task LoadMediaAsync(string filePath)
        {
            if(_currentImageUrl != null)
            {
                await JSRuntime.InvokeAsync<string>("revokeObjectURL", _currentImageUrl);
            }

            using var stream = File.Open(filePath, FileMode.Open);
            var dotnetImageStream = new DotNetStreamReference(stream);
            _currentImageUrl = await JSRuntime.InvokeAsync<string>("createObjectURL", dotnetImageStream);

            await InvokeAsync(StateHasChanged);
        }
    }
}
