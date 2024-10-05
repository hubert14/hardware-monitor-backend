using HardwareMon.UI.Settings;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace HardwareMon.UI.Services
{
    public class HomeAssistantDataViewModel
    {
        public string Temperature { get; set; } = "0";
        public string Co2 { get; set; } = "0";
        public bool Alert { get; set; }
    }

    public class SensorResponse
    {
        [JsonPropertyName("entity_id")]
        public string? EntityId { get; set; }

        [JsonPropertyName("state")]
        public string? State { get; set; }
    }

    internal class HomeAssistantService : RefreshableService<HomeAssistantDataViewModel>
    {
        private readonly HttpClient _client;

        private readonly string _temperature;
        private readonly string _co2;
        private readonly string _alerts;

        protected override int RetrieveDelayMillis => 5_000;

        public HomeAssistantService(AppSettings settings)
        {
            _client = new()
            {
                BaseAddress = new Uri(settings.Hass.ApiUrl)
            };

            _client.DefaultRequestHeaders.Authorization = new("Bearer", settings.Hass.ApiKey);

            _temperature = settings.Hass.Sensors.Temperature;
            _co2 = settings.Hass.Sensors.Co2;
            _alerts = settings.Hass.Sensors.Alerts;
        }

        protected override async Task<HomeAssistantDataViewModel> ProcessRetrieveDataAsync()
        {
            try
            {
                var temperature = await _client.GetFromJsonAsync<SensorResponse>("api/states/" + _temperature);
                var co2 = await _client.GetFromJsonAsync<SensorResponse>("api/states/" + _co2);
                var alerts = await _client.GetFromJsonAsync<SensorResponse>("api/states/" + _alerts);

                return new HomeAssistantDataViewModel
                {
                    Temperature = temperature.State,
                    Co2 = co2.State,
                    Alert = alerts.State == "on"
                };
            }
            catch(Exception e)
            {
                await Console.Out.WriteLineAsync(e.Message);
                return null;
            }
            
        }
    }
}
