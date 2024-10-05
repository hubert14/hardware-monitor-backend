using HardwareMon.UI.Settings;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace HardwareMon.UI.Services
{
    class GetPlayerSummaries
    {
        [JsonPropertyName("response")]
        public GetPlayerSummariesResponse Response { get; set; }
    }

    class GetPlayerSummariesResponse
    {
        [JsonPropertyName("players")]
        public List<SteamProfile> Players { get; set; }
    }

    class SteamProfile
    {
        [JsonPropertyName("steamid")]
        public string? SteamId { get; set; }

        [JsonPropertyName("personaname")]
        public string? Name { get; set; }

        [JsonPropertyName("avatar")]
        public string? Avatar { get; set; }

        [JsonPropertyName("personastate")]
        public int Status { get; set; }

        [JsonPropertyName("gameextrainfo")]
        public string? GameTitle { get; set; }

        public string StatusTitle
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(GameTitle)) return "playing";
                if (Status != 0) return "online";
                if (Status == -1) return "unknown";

                return "offline";
            }
        }
    }

    class SteamDataViewModel
    {
        public List<SteamProfile> Profiles { get; set; } = new();
    }

    internal class SteamService : RefreshableService<SteamDataViewModel>
    {
        protected override int RetrieveDelayMillis => 60_000;

        private readonly HttpClient _httpClient;
        private readonly string _query;

        public SteamService(AppSettings settings)
        {
            _httpClient = new();
            _httpClient.BaseAddress = new Uri("https://api.steampowered.com");
            _query = $"?key={settings.Steam.ApiKey}&steamIds={string.Join(',', settings.Steam.SteamIDs)}";
        }

        protected override async Task<SteamDataViewModel> ProcessRetrieveDataAsync()
        {
            //return new SteamDataViewModel
            //{
            //    Profiles = new List<SteamProfile>
            //    {
            //        new SteamProfile
            //        {
            //            Status = 1,
            //            Name = "huber",
            //            SteamId = "8858228",
            //            Avatar = "https://google.com/favicon.ico"
            //        },
            //        new SteamProfile
            //        {
            //            Status = 1,
            //            Name = "hubert",
            //            SteamId = "8388228",
            //            Avatar = "https://google.com/favicon.ico"
            //        },
            //        new SteamProfile
            //        {
            //            Status = 1,
            //            Name = "hubeur",
            //            SteamId = "8828228",
            //            Avatar = "https://google.com/favicon.ico"
            //        },
            //        new SteamProfile
            //        {
            //            Status = 1,
            //            Name = "huberi",
            //            SteamId = "8881228",
            //            Avatar = "https://google.com/favicon.ico"
            //        }
            //    }
            //};

            const string players_uri = "ISteamUser/GetPlayerSummaries/v0002/";
            var response = await _httpClient.GetFromJsonAsync<GetPlayerSummaries>(players_uri + _query);
            if (response == null) return new();
            return new SteamDataViewModel { Profiles = response.Response.Players.OrderBy(x => x.Name).ToList() };
        }
    }
}
