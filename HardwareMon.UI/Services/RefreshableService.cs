namespace HardwareMon.UI.Services
{
    abstract class RefreshableService<T>
    {
        public event AsyncDataEventHandler<T>? NewDataArrivedEvent;

        private Timer? _timer;

        private T? _lastRetrievedData;

        protected abstract int RetrieveDelayMillis { get; }

        public void StartDataRecieving() => _timer = new Timer(async _ => await RetrieveDataAsync(), state: null, dueTime: 5_000, period: RetrieveDelayMillis);
        public void StopDataRecieving() => _timer?.Dispose();

        public async Task RetrieveDataAsync()
        {
            try
            {
                var data = await ProcessRetrieveDataAsync();
                _lastRetrievedData = data;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            if (_lastRetrievedData != null) NewDataArrivedEvent?.Invoke(this, _lastRetrievedData);
        }

        protected abstract Task<T> ProcessRetrieveDataAsync();
    }
}
