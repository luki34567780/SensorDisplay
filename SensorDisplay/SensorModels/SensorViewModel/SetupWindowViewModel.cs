using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorDisplay.SensorModels.SensorViewModel
{
    internal partial class SetupWindowViewModel : ObservableObject
    {
        public Action Exit { get; set; }
        public string ComPort { get; private set; }
        public int Baudrate { get; private set; }

        public TrulyObservableCollection<ComPortViewModel> ComPorts { get; set; } = new();
        private ComPortViewModel _selectedModel;

        public ComPortViewModel SelectedModel
        {
            get => _selectedModel;
            set => SetProperty(ref _selectedModel, value);
        }

        private int _numericUpDownValue = 9600;
        public int NumericUpDownValue
        {
            get => _numericUpDownValue;
            set => SetProperty(ref _numericUpDownValue, value);
        }

        public SetupWindowViewModel()
        {
            foreach (var port in SerialPort.GetPortNames())
                ComPorts.Add(new ComPortViewModel(port));

            SelectedModel = ComPorts.Last();
        }

        [RelayCommand]
        public void NumericUpDownUp()
        {
            NumericUpDownValue++;
        }

        [RelayCommand]
        public void NumericUpDownDown()
        {
            NumericUpDownValue--;
        }

        [RelayCommand]
        public void Save()
        {
            ComPort = SelectedModel.Name;
            Baudrate = NumericUpDownValue;
            Exit();
        }
    }
}
