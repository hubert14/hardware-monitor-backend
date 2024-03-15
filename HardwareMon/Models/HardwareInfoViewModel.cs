namespace HardwareMon.Models
{
    public class HardwareInfoViewModel
    {
        public CPUViewModel CPU { get; }
        public GPUViewModel GPU { get; }
        public MotherboardViewModel Motherboard { get; }
        public RAMViewModel RAM { get; }
        public ROMViewModel ROM { get; }

        public HardwareInfoViewModel(CollectingData? data)
        {
            CPU = new CPUViewModel(data?.CPUInfo);
            GPU = new GPUViewModel(data?.GPUInfo);
            Motherboard = new MotherboardViewModel(data?.MBInfo);
            RAM = new RAMViewModel(data?.RAMInfo);
            ROM = new ROMViewModel(data?.ROMInfo);
        }
    }

    public class CPUViewModel
    {
        public string PowerUsage { get; }
        public string Temperature { get; }
        public string Voltage { get; }
        public string Clock { get; }
        public string FanRPM { get; }
        public string Usage { get; }

        public CPUViewModel(CPUInfo? cpu)
        {
            PowerUsage = cpu?.PowerUsage?.ToString("##0") ?? "0";
            Temperature = cpu?.Temperature?.ToString("##0") ?? "0";
            Voltage = cpu?.Voltage?.ToString("0.000") ?? "0.000";
            Clock = cpu?.ClockMhz?.ToString("#000") ?? "0";
            FanRPM = cpu?.FanRPM?.ToString("###0") ?? "0";
            Usage = cpu?.Utilization?.ToString("0") ?? "0";
        }
    }

    public class GPUViewModel
    {
        public string PowerUsage { get; }
        public string Temperature { get; }
        public string HotspotTemperature { get; }
        public string FanRPM { get; }
        public string Usage { get; }
        public string MemoryUsage { get; }

        public GPUViewModel(GPUInfo? gpu)
        {
            PowerUsage = gpu?.PowerUsage?.ToString("##0") ?? "0";
            Temperature = gpu?.Temperature?.ToString("##0") ?? "0";
            HotspotTemperature = gpu?.HotspotTemperature?.ToString("##0") ?? "0";
            FanRPM = gpu?.FanRPM?.ToString("###0") ?? "0";
            Usage = gpu?.Utilization?.ToString("0") ?? "0";
            MemoryUsage = gpu?.MemoryUtilization?.ToString("##0") ?? "0";
        }
    }

    public class MotherboardViewModel
    {
        public string FirstTemperature { get; }
        public string SecondTemperature { get; }

        public string FanRPM { get; }

        public MotherboardViewModel(MBInfo? mb)
        {
            FirstTemperature = mb?.Temperatures?.FirstOrDefault()?.ToString("##0") ?? "0";
            SecondTemperature = mb?.Temperatures?.LastOrDefault()?.ToString("##0") ?? "0";

            FanRPM = mb?.FanRPM?.ToString("###0") ?? "0";
        }
    }

    public class RAMViewModel
    {
        public string Usage { get; }

        public RAMViewModel(RAMInfo? ram)
        {
            Usage = ram?.Utilization?.ToString("##0") ?? "0";
        }
    }

    public class ROMViewModel
    {
        public string FirstTemperature { get; }
        public string SecondTemperature { get; }

        public ROMViewModel(List<ROMInfo>? ram)
        {
            FirstTemperature = ram?.FirstOrDefault()?.Temperature?.ToString("##0") ?? "0";
            SecondTemperature = ram?.LastOrDefault()?.Temperature?.ToString("##0") ?? "0";
        }
    }
}
