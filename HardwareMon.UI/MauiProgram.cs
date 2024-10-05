using HardwareMon.UI.Services;
using HardwareMon.UI.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Maui.LifecycleEvents;
using Microsoft.UI.Composition.SystemBackdrops;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml.Media;
using Windows.UI.WebUI;
using WinUIEx;

namespace HardwareMon.UI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            var settings = new AppSettings();

            builder.Configuration
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.local.json", optional: true)
                .Build()
                .Bind(settings);
            builder.Services.AddSingleton(settings);

            builder
                .UseMauiApp<App>()
                .ConfigureLifecycleEvents(events =>
                {
                    events.AddWindows(wndLifeCycleBuilder =>
                    {
                        wndLifeCycleBuilder.OnWindowCreated(window =>
                        {
                            window.SystemBackdrop = new TransparentTintBackdrop();
                            window.ExtendsContentIntoTitleBar = false;
                            var appWindow = window.AppWindow;
                            
                            appWindow.IsShownInSwitchers = false;
                            appWindow.MoveAndResize(new(settings.Window.OffsetX, settings.Window.OffsetY, settings.Window.Width, settings.Window.Height));

                            var presenter = window.AppWindow.Presenter as OverlappedPresenter;
                            presenter?.SetBorderAndTitleBar(hasTitleBar: false, hasBorder: false);
                            presenter?.Maximize();
                        });
                    });
                });

            builder.Services.AddSingleton<HardwareService>();
            builder.Services.AddSingleton<HomeAssistantService>();
            builder.Services.AddSingleton<SteamService>();
            builder.Services.AddSingleton<WallpaperService>();
            builder.Services.AddSingleton<LinksService>();

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
#endif

            return builder.Build();
        }
    }
}
