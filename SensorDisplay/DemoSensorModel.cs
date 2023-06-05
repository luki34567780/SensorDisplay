using CommunityToolkit.Mvvm.ComponentModel;
using SensorDisplay.SensorModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorDisplay
{
    class DemoSensorModel : ObservableObject, ISensorViewModel
    {
        public string SensorName => nameof(DemoSensorModel);
        public int Co2Ppm { get => 5; set => throw new NotImplementedException(); }
        public bool Co2PpmAvailable { get => true; set => throw new NotImplementedException(); }
        public double CpuFrequency { get => 25; set => throw new NotImplementedException(); }
        public bool CPUFrequencyAvailable { get => true; set => throw new NotImplementedException(); }
        public double CpuTemperature { get => 55; set => throw new NotImplementedException(); }
        public bool CpuTemperatureAvailable { get => true; set => throw new NotImplementedException(); }
        public int FontSize { get => 30; set => throw new NotImplementedException(); }
        public double Humidity { get => 30; set => throw new NotImplementedException(); }
        public bool HumidityAvailable { get => true; set => throw new NotImplementedException(); }
        public double Temperature { get => 30; set => throw new NotImplementedException(); }
        public bool TemperatureAvailable { get => true; set => throw new NotImplementedException(); }
        public double TVOC { get => 30; set => throw new NotImplementedException(); }
        public bool TVOCAvailable { get => true; set => throw new NotImplementedException(); }
        public double WaitTime { get => 30; set => throw new NotImplementedException(); }
        public bool WaitTimeAvailable { get => true; set => throw new NotImplementedException(); }

        public string ItemDescription => throw new NotImplementedException();
    }
}
