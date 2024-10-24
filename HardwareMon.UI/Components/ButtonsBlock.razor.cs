using HardwareMon.UI.Services;
using Microsoft.AspNetCore.Components;
using static HardwareMon.UI.Settings.AppSettings;

namespace HardwareMon.UI.Components
{
    public partial class ButtonsBlock
    {
        [Inject] private ButtonsService ButtonsService { get; set; }

        List<ButtonInfo> _buttons;

        protected override void OnInitialized()
        {

            _buttons = ButtonsService.Buttons;
            base.OnInitialized();
        }
    }
}
