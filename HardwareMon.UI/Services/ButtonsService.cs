using HardwareMon.UI.Settings;
using static HardwareMon.UI.Settings.AppSettings;

namespace HardwareMon.UI.Services
{
    internal class ButtonsService
    {
        public List<ButtonInfo> Buttons { get; }

        public ButtonsService(AppSettings settings)
        {
            Buttons = settings.Buttons.Items;
        }
    }
}
