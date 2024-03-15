namespace HardwareMon.UI.Settings
{
    internal class AppSettings
    {
        public HassSettings Hass { get; set; }
        public WindowSettings Window { get; set; }
        public SteamSettings Steam { get; set; }
        public WallpaperSettings Wallpaper { get; set; }

        public class HassSettings
        {
            public string ApiUrl { get; set; }
            public string ApiKey { get; set; }

            public HassSensorsSettings Sensors { get; set; }

            public class HassSensorsSettings
            {
                public string Co2 { get; set; }
                public string Alerts { get; set; }
            }
        }

        public class WindowSettings
        {
            public int OffsetX { get; set; }
            public int OffsetY { get; set; }
            public int Width { get; set; }
            public int Height { get; set; }
        }

        public class SteamSettings
        {
            public string ApiKey { get; set; }
            public string[] SteamIDs { get; set; }
        }

        public class WallpaperSettings
        {
            public string FolderPath { get; set; }
            public int ChangeTimerSeconds { get; set; }
        }
    }
}
