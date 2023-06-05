using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SensorDisplay.SensorModels.SensorViewModel
{
    class SensorViewModel : ObservableObject, ISensorViewModel
    {
        public static async Task<ISensorViewModel> CreateNew()
        {
            var window = new SetupWindow();
            window.Show();

            bool finished = false;
            EventHandler lambda = (object? _, EventArgs _2) =>
            { 
                finished = true; 
            };
            window.Closed += lambda;

            while (!finished)
            {
                await Task.Delay(1);
            }

            var model = (SetupWindowViewModel)window.DataContext;

            return new SensorViewModel(model.ComPort, model.Baudrate);
        }

        public int FontSize { get; set; } = 25;
        private int _co2Ppm = 0;
        private SensorReader _sensors;

        private bool _co2PpmAvailble;

        public string SensorName => nameof(SensorViewModel);
        public bool Co2PpmAvailable
        {
            get => _co2PpmAvailble;
            set => SetProperty(ref _co2PpmAvailble, value);
        }

        public int Co2Ppm
        {
            get => _co2Ppm;
            set => SetProperty(ref _co2Ppm, value);
        }

        private bool _temperatureAvailable;

        public bool TemperatureAvailable
        {
            get => _temperatureAvailable;
            set => SetProperty(ref _temperatureAvailable, value);
        }

        private double _temperature = 0;

        public double Temperature
        {
            get => _temperature;
            set => SetProperty(ref _temperature, value);
        }

        private bool _humidityAvailable;

        public bool HumidityAvailable
        {
            get => _humidityAvailable;
            set => SetProperty(ref _humidityAvailable, value);
        }

        private double _humidity = 0;

        public double Humidity
        {
            get => _humidity;
            set => SetProperty(ref _humidity, value);
        }

        private bool _tvocAvailable;

        public bool TVOCAvailable
        {
            get => _tvocAvailable;
            set => SetProperty(ref _tvocAvailable, value);
        }

        private double _tvoc = 0;

        public double TVOC
        {
            get => _tvoc = 0;
            set => SetProperty(ref _tvoc, value);
        }

        private bool _cpuTemperatureAvailable;

        public bool CpuTemperatureAvailable
        {
            get => _cpuTemperatureAvailable;
            set => SetProperty(ref _cpuTemperatureAvailable, value);
        }

        private double _cpuTemperature = 0;

        public double CpuTemperature
        {
            get => _cpuTemperature;
            set => SetProperty(ref _cpuTemperature, value);
        }

        private bool _cpuFrequencyAvailable;

        public bool CPUFrequencyAvailable
        {
            get => _cpuTemperatureAvailable;
            set => SetProperty(ref _cpuTemperatureAvailable, value);
        }

        private double _cpuFrequency = 125;

        public double CpuFrequency
        {
            get => _cpuFrequency;
            set => SetProperty(ref _cpuFrequency, value);
        }

        private bool _waitTimeAvailable;

        public bool WaitTimeAvailable
        {
            get => _waitTimeAvailable;
            set => SetProperty(ref _waitTimeAvailable, value);
        }

        private double _waitTime = 0;

        public double WaitTime
        {
            get => _waitTime;
            set => SetProperty(ref _waitTime, value);
        }

        public string ItemDescription => throw new NotImplementedException();

        public SensorViewModel(string port, int baudrate)
        {
            _sensors = new SensorReader(port, baudrate);
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
