namespace HardwareMon.UI.Components.Pages
{
    enum SelectedScreen { Loading, Hardware, Alerts, Links }

    public partial class MainPage
    {
        private WallpaperBlock _wallpaperBlockRef;

        private SelectedScreen? _previousScreen;
        private SelectedScreen _selectedScreen = SelectedScreen.Hardware;
        private bool _currentAlertState;

        private void OnToggleAlert() => ChangeScreen(SelectedScreen.Alerts);
        private void OnToggleLink() => ChangeScreen(SelectedScreen.Links);

        private void ChangeScreen(SelectedScreen screen) => _selectedScreen = _selectedScreen == screen ? SelectedScreen.Hardware : screen;

        private void OnClickPrevious()
        {
            _wallpaperBlockRef.SetPrevious();
        }

        private void OnClickNext()
        {
            _wallpaperBlockRef.SetNext();
        }

        private void OnAlertStateChanged(bool state)
        {
            var valueChanged = state != _currentAlertState;
            _currentAlertState = state;
            if (valueChanged) InvokeAsync(StateHasChanged);
        }
    }
}
