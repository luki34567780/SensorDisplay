using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SensorDisplay.SensorModels;
using SensorDisplay.SensorModels.SensorViewModel;

namespace SensorDisplay
{
    internal partial class MainViewModel : ObservableObject
    {
        private int _currentIndex;
        public int CurrentIndex
        {
            get => _currentIndex;
            set
            {
                SetProperty(ref _currentIndex, value);
                CurrentSensor = Sensors[value];
            }
        }
        public List<ISensorViewModel> Sensors { get; set; } = new();

        private ISensorViewModel _currentSensor;
        public ISensorViewModel CurrentSensor 
        {
            get => _currentSensor;
            set => SetProperty(ref _currentSensor, value);
        }

        public MainViewModel()
        {
        }

        [RelayCommand]
        public void UpPressed()
        {
            if (CurrentIndex == Sensors.Count || Sensors.Count == 0)
                return;
            CurrentIndex++;
        }

        [RelayCommand] 
        public void DownPressed()
        {
            if (CurrentIndex == 0)
                return;

            CurrentIndex--;
        }

        [RelayCommand]
        public async void AddNewSerialSensor()
        {
            var sensor = await SensorViewModel.CreateNew();
            Sensors.Add(sensor);

            if (CurrentSensor == null)
                CurrentSensor = sensor;

        }
    }
}
