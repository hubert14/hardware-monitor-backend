namespace HardwareMon.Models
{
    public class CollectingData
    {
        public MBInfo MBInfo { get; set; } = new();
        public CPUInfo CPUInfo { get; set; } = new();
        public GPUInfo GPUInfo { get; set; } = new();
        public RAMInfo RAMInfo { get; set; } = new();
        public List<ROMInfo> ROMInfo { get; set; } = new();

        public List<NetInfo> NetInfo { get; set; } = new();
    }

    public class MBInfo
    {
        public List<float?> Temperatures { get; set; } = new();
        public float? FanRPM { get; set; }
    }

    public class CPUInfo
    {
        public float? Temperature { get; set; }
        public float? Utilization { get; set; }
        public float? PowerUsage { get; set; }
        public float? ClockMhz { get; set; }
        public float? Voltage { get; set; }
        public float? FanRPM { get; set; }

    }

    public class GPUInfo
    {
        public float? Temperature { get; set; }
        public float? HotspotTemperature { get; set; }
        public float? Utilization { get; set; }
        public float? MemoryUtilization { get; set; }
        public float? PowerUsage { get; set; }
        public float? FanRPM { get; set; }
    }

    public class RAMInfo
    {
        public float? Utilization { get; set; }
    }

    public class ROMInfo
    {
        public string Name { get; set; }
        public float? Temperature { get; set; }
        public float? UsedSpace { get; set; }
    }

    public class NetInfo
    {
        public string Name { get; set; }
        public float? UpSpeed { get; set; }
        public float? DownSpeed { get; set; }
    }
}
