using HardwareMon.Models;

namespace HardwareMon.UI.Services
{
    public delegate void DataEventHandler<T>(object sender, T data);
    public delegate Task AsyncDataEventHandler<T>(object sender, T data);

    internal class HardwareService : BaseService<CollectingData>
    {
        private static readonly Processor _processor = new();

        protected override int RetrieveDelayMillis => 1_000;

        protected override async Task<CollectingData> ProcessRetrieveDataAsync()
        {
            return _processor.GetHardwareData();
        }
    }
}
