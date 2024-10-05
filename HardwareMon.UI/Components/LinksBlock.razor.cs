using HardwareMon.UI.Services;
using Microsoft.AspNetCore.Components;
using static HardwareMon.UI.Settings.AppSettings;

namespace HardwareMon.UI.Components
{
    public partial class LinksBlock
    {
        [Inject] private LinksService LinksService { get; set; }

        [Parameter] public EventCallback Click { get; set; }

        private List<Link> _links = new();
        private string[] _namespaces;

        protected override void OnInitialized()
        {
            _links = LinksService.Links;
            _namespaces = LinksService.Namespaces;

            base.OnInitialized();
        }

        private async Task CopyToClipboardAsync(string text)
        {
            await Clipboard.Default.SetTextAsync(text);
            await TriggerClick();
        }

        private async Task TriggerClick()
        {
            await Click.InvokeAsync();
        }
    }
}
