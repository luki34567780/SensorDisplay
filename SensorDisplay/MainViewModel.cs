using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace SensorDisplay
{
    internal partial class MainViewModel : ObservableObject
    {
        public int FontSize { get; set; } = 30;
        private int _co2Ppm = 0;
        private SensorReader _sensors;

        public int Co2Ppm
        {
            get => _co2Ppm;
            set => SetProperty(ref _co2Ppm, value);
        }

        private double _temperature = 0;
        
        public double Temperature
        {
            get => _temperature;
            set => SetProperty(ref _temperature, value);
        }

        private double _humidity = 0;

        public double Humidity
        {
            get => _humidity;
            set => SetProperty(ref _humidity, value);
        }

        private double _tvoc = 0;

        public double TVOC
        {
            get => _tvoc = 0;
            set => SetProperty(ref _tvoc, value);
        }

        private double _cpuTemperature = 0;

        public double CpuTemperature
        {
            get => _cpuTemperature;
            set => SetProperty(ref _cpuTemperature, value);
        }

        private double _cpuFrequency = 125;

        public double CpuFrequency
        {
            get => _cpuFrequency;
            set => SetProperty(ref _cpuFrequency, value);
        }

        private double _waitTime = 0;

        public double WaitTime
        {
            get => _waitTime;
            set => SetProperty(ref _waitTime, value);
        }

        public MainViewModel() 
        {
            _sensors = new SensorReader("COM13", 9600);
            _sensors.NewValuesAvailableEvent += NewValuesAvailable;
        }

        private void NewValuesAvailable(object? sender, DataPacket packet)
        {
            Co2Ppm = (int)packet.Co2PPM;
            Temperature = packet.TemperatureSensor;
            Humidity = packet.Humidity;
            TVOC = packet.TVOC;
            CpuTemperature = packet.CpuTemperatureSensor;
            CpuFrequency = packet.CpuFrequencyMhz;
            WaitTime = packet.IdleTimeMs;
        }
    }
}
