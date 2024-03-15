namespace HardwareMon.UI.Components.Pages
{
    enum SelectedScreen { Loading, Hardware, Alerts }

    public partial class MainPage
    {
        private WallpaperBlock _wallpaperBlockRef;

        private SelectedScreen _selectedScreen = SelectedScreen.Hardware;
        private bool _currentAlertState;

        private void OnToggleAlert()
        {
            _selectedScreen =
                _selectedScreen == SelectedScreen.Alerts
                ? SelectedScreen.Hardware
                : SelectedScreen.Alerts;
        }

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
            _currentAlertState = state;
        }
    }
}
