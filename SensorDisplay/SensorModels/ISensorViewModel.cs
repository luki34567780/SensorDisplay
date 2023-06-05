using System;
using System.ComponentModel;

namespace SensorDisplay.SensorModels
{
    internal interface ISensorViewModel : INotifyPropertyChanged
    {
        public static ISensorViewModel CreateNew() { throw new NotImplementedException(); }
        
        string ItemDescription { get; }
        string SensorName { get; }
        int Co2Ppm { get; set; }
        bool Co2PpmAvailable { get; set; }
        double CpuFrequency { get; set; }
        bool CPUFrequencyAvailable { get; set; }
        double CpuTemperature { get; set; }
        bool CpuTemperatureAvailable { get; set; }
        int FontSize { get; set; }
        double Humidity { get; set; }
        bool HumidityAvailable { get; set; }
        double Temperature { get; set; }
        bool TemperatureAvailable { get; set; }
        double TVOC { get; set; }
        bool TVOCAvailable { get; set; }
        double WaitTime { get; set; }
        bool WaitTimeAvailable { get; set; }
    }
}