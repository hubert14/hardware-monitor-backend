using HardwareMon.UI.Settings;
using static HardwareMon.UI.Settings.AppSettings;

namespace HardwareMon.UI.Services
{
    internal class LinksService
    {
        public List<Link> Links { get; }
        public string[] Namespaces { get; }

        public LinksService(AppSettings settings)
        {
            Links = settings.Links.Items;
            Namespaces = settings.Links.Namespaces;
        }
    }
}
