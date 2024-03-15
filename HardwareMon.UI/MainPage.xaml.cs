using WinUIEx;
using static HardwareMon.UI.WinUI.App;

namespace HardwareMon.UI
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnHandlerChanged()
        {
            base.OnHandlerChanged();

            var window = App.Current.Windows.First<Window>().Handler.PlatformView as Microsoft.UI.Xaml.Window;
            IntPtr hWnd = WinRT.Interop.WindowNative.GetWindowHandle(window);
            int exStyle = (int)GetWindowLong(hWnd, (int)GetWindowLongFields.GWL_EXSTYLE);
            exStyle |= (int)ExtendedWindowStyles.WS_EX_TOOLWINDOW;
            SetWindowLong(hWnd, (int)GetWindowLongFields.GWL_EXSTYLE, exStyle);
        }
    }
}
