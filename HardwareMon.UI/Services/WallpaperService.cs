using HardwareMon.UI.Settings;

namespace HardwareMon.UI.Services
{
    internal class WallpaperService
    {

        public event DataEventHandler<string> ImageChangedEvent;

        private readonly string[] _images;

        private int _currentImageIndex;
        public int CurrentImageIndex
        {
            get => _currentImageIndex;
            set
            {
                _currentImageIndex = value < 0 || value >= _images.Length ? 0 : value;
                UpdateImage();
            }
        }

        private Timer _timer;

        public WallpaperService(AppSettings settings)
        {
            _images = Directory.EnumerateFiles(settings.Wallpaper.FolderPath).ToArray();
            var updateImageTime = TimeSpan.FromSeconds(settings.Wallpaper.ChangeTimerSeconds);
            _timer = new Timer(_ => CurrentImageIndex++, null, TimeSpan.FromSeconds(5), updateImageTime);
        }

        private void UpdateImage()
        {
            ImageChangedEvent.Invoke(this, _images[CurrentImageIndex]);
        }
    }
}
