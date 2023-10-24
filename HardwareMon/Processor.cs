using HardwareMon.Models;
using LibreHardwareMonitor.Hardware;

namespace HardwareMon
{
    public class Processor
    {
        private readonly Computer _computer;

        public Processor()
        {
            _computer = new Computer
            {
                IsCpuEnabled = true,
                IsGpuEnabled = true,
                IsMemoryEnabled = true,
                IsMotherboardEnabled = true,
                IsNetworkEnabled = false, // TODO: Add network collecting
                IsStorageEnabled = true,
            };

            Console.WriteLine("Processor initialized");
        }

        public CollectingData GetHardwareData()
        {
            var result = new CollectingData();

            _computer.Open();
            _computer.Accept(new UpdateVisitor());

            var motherboardSensors = _computer.Hardware.First(x => x.HardwareType == HardwareType.Motherboard).SubHardware[0].Sensors;
            foreach (var sensor in motherboardSensors)
            {
                switch ((sensor.SensorType, sensor.Name))
                {
                    case (SensorType.Voltage, "Vcore"): result.CPUInfo.Voltage = sensor.Value; break;
                    case (SensorType.Fan, "Fan #2"): result.CPUInfo.FanRPM = sensor.Value; break;
                    case (SensorType.Fan, "Fan #1"): result.MBInfo.FanRPM = sensor.Value; break;
                    case (SensorType.Temperature, "CPU Core"): result.CPUInfo.Temperature = sensor.Value; break;
                    case (SensorType.Temperature, "Temperature #1"): result.MBInfo.Temperatures.Add(sensor.Value); break;
                    case (SensorType.Temperature, "Temperature #3"): result.MBInfo.Temperatures.Add(sensor.Value); break;
                }
            }

            var cpuSensors = _computer.Hardware.First(x => x.HardwareType == HardwareType.Cpu).Sensors;
            foreach (var sensor in cpuSensors)
            {
                switch ((sensor.SensorType, sensor.Name))
                {
                    case (SensorType.Load, "CPU Total"): result.CPUInfo.Utilization = sensor.Value; break;
                    case (SensorType.Power, "CPU Cores"): result.CPUInfo.PowerUsage = sensor.Value; break;
                    case (SensorType.Clock, "CPU Core #1"): result.CPUInfo.ClockMhz = sensor.Value; break;
                }
            }

            var gpuSensors = _computer.Hardware.First(x => x.HardwareType == HardwareType.GpuNvidia).Sensors;
            foreach (var sensor in gpuSensors)
            {
                switch ((sensor.SensorType, sensor.Name))
                {
                    case (SensorType.Temperature, "GPU Core"): result.GPUInfo.Temperature = sensor.Value; break;
                    case (SensorType.Temperature, "GPU Hot Spot"): result.GPUInfo.HotspotTemperature = sensor.Value; break;
                    case (SensorType.Fan, "GPU Fan 1"): result.GPUInfo.FanRPM = sensor.Value; break;
                    case (SensorType.Load, "GPU Core"): result.GPUInfo.Utilization = sensor.Value; break;
                    case (SensorType.Load, "GPU Memory"): result.GPUInfo.MemoryUtilization = sensor.Value; break;
                    case (SensorType.Power, "GPU Package"): result.GPUInfo.PowerUsage = sensor.Value; break;
                }
            }

            var ramSensors = _computer.Hardware.First(x => x.HardwareType == HardwareType.Memory).Sensors;
            foreach (var sensor in ramSensors)
            {
                switch ((sensor.SensorType, sensor.Name))
                {
                    case (SensorType.Load, "Memory"): result.RAMInfo.Utilization = sensor.Value; break;
                    case (SensorType.Temperature, "GPU Hot Spot"): result.GPUInfo.HotspotTemperature = sensor.Value; break;
                }
            }

            var roms = _computer.Hardware.Where(x => x.HardwareType == HardwareType.Storage);
            foreach (var rom in roms)
            {
                var romItem = new ROMInfo { Name = rom.Name };

                foreach (var sensor in rom.Sensors)
                {
                    switch ((sensor.SensorType, sensor.Name))
                    {
                        case (SensorType.Temperature, "Temperature 2"): romItem.Temperature = sensor.Value; break;
                        case (SensorType.Load, "Used Space"): romItem.UsedSpace = sensor.Value; break;
                    }
                }

                result.ROMInfo.Add(romItem);
            }

            // TODO: Check UpSpeed collecting (now it doesn't collect)
            //var networks = _computer.Hardware.Where(x => x.HardwareType == HardwareType.Network);
            //foreach (var net in networks)
            //{
            //    var netItem = new NetInfo { Name = net.Name };

            //    foreach (var sensor in net.Sensors)
            //    {
            //        switch ((sensor.SensorType, sensor.Name))
            //        {
            //            case (SensorType.Throughput, "Download Speed"): netItem.DownSpeed = sensor.Value; break;
            //            case (SensorType.Throughput, "Uplaod Speed"): netItem.UpSpeed = sensor.Value; break;
            //        }
            //    }

            //    result.NetInfo.Add(netItem);
            //}

            _computer.Close();

            return result;
        }
    }
}
