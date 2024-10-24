using Microsoft.AspNetCore.Components.Web;

namespace HardwareMon.UI.Components.Pages
{
    enum SelectedScreen { Loading, Hardware, Alerts, Links }
    enum SelectedTopBlock { Friends, Services }

    public partial class MainPage
    {
        private SelectedScreen _selectedScreen = SelectedScreen.Hardware;
        private SelectedTopBlock _selectedTopBlock = SelectedTopBlock.Friends;

        private bool _currentAlertState;

        private void OnToggleAlert() => ChangeScreen(SelectedScreen.Alerts);
        private void OnToggleLink() => ChangeScreen(SelectedScreen.Links);

        private void ChangeScreen(SelectedScreen screen) => _selectedScreen = _selectedScreen == screen ? SelectedScreen.Hardware : screen;

        private void OnAlertStateChanged(bool state)
        {
            var valueChanged = state != _currentAlertState;
            _currentAlertState = state;
            if (valueChanged) InvokeAsync(StateHasChanged);
        }

        private void OnSelectedBlockChanged(WheelEventArgs args)
        {
            var isUpScroll = args.DeltaY < 0;

            _selectedTopBlock = _selectedTopBlock switch
            {
                SelectedTopBlock.Friends => SelectedTopBlock.Services,
                SelectedTopBlock.Services => SelectedTopBlock.Friends,
                _ => throw new NotImplementedException()
            };
            
            InvokeAsync(StateHasChanged);
        }
    }
}
