namespace HardwareMon.UI.Settings
{
    internal class AppSettings
    {
        public HassSettings Hass { get; set; }
        public WindowSettings Window { get; set; }
        public SteamSettings Steam { get; set; }
        public ButtonSettings Buttons { get; set; }
        public LinkSettings Links { get; set; }

        public class HassSettings
        {
            public string ApiUrl { get; set; }
            public string ApiKey { get; set; }

            public HassSensorsSettings Sensors { get; set; }

            public class HassSensorsSettings
            {
                public string Temperature { get; set; }
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

        public class ButtonSettings
        {
            public List<ButtonInfo> Items { get; set; }
        }

        public class ButtonInfo
        {
            public string Title { get; set; }
            public string Color { get; set; }
            public string Image { get; set; }
            public string Url { get; set; }
        }

        public class LinkSettings
        {
            public List<Link> Items { get; set; }
            public string[] Namespaces { get; set; }
        }

        public class Link
        {
            public string Title { get; set; }
            public string? TitleLink { get; set; }
            public string? Port { get; set; }
            public string Url { get; set; }
        }
    }
}
