using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace HardwareMon.Grpc.Services
{
    public class HardwareService : Grpc.HardwareService.HardwareServiceBase
    {
        private readonly Processor _processor;

        public HardwareService(Processor processor)
        {
            _processor = processor;
        }

        public override async Task<CollectingData> MonitorData(Empty request, ServerCallContext context)
        {
            try
            {
                var res = _processor.GetHardwareData();
                var map = new CollectingData
                {
                    MBInfo = new MBInfo
                    {
                        FanRPM = res.MBInfo.FanRPM,
                    },
                    CPUInfo = new CPUInfo
                    {
                        ClockMhz = res.CPUInfo.ClockMhz,
                        FanRPM = res.CPUInfo.FanRPM,
                        PowerUsage = res.CPUInfo.PowerUsage,
                        Temperature = res.CPUInfo.Temperature,
                        Utilization = res.CPUInfo.Utilization,
                        Voltage = res.CPUInfo.Voltage,
                    },
                    GPUInfo = new GPUInfo
                    {
                        FanRPM = res.GPUInfo.FanRPM,
                        HotspotTemperature = res.GPUInfo.HotspotTemperature,
                        Temperature = res.GPUInfo.Temperature,
                        MemoryUtilization = res.GPUInfo.MemoryUtilization,
                        PowerUsage = res.GPUInfo.PowerUsage,
                        Utilization = res.GPUInfo.Utilization
                    },
                    RAMInfo = new RAMInfo
                    {
                        Utilization = res.RAMInfo.Utilization,
                    }
                };

                res.MBInfo.Temperatures.ForEach(map.MBInfo.Temperatures.Add);
                res.ROMInfo.ForEach(x => map.ROMInfo.Add(new ROMInfo { Name = x.Name, Temperature = x.Temperature, UsedSpace = x.UsedSpace }));
                // TODO: Add network data
                //res.NetInfo.ForEach(x => map.NetInfo.Add(new NetInfo { Name = x.Name, UpSpeed = x.UpSpeed, DownSpeed = x.DownSpeed }));

                return map;
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                return new CollectingData();
            }

        }
    }
}