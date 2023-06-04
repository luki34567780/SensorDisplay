using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace SensorDisplay
{
    internal partial class MainViewModel : ObservableObject
    {
        private int _co2Ppm = 0;
        private SensorReader _sensors;

        private int _radius = 250;
        public int Radius
        {
            get => _radius;
            set => SetProperty(ref _radius, value);
        }

        private int _scaleRadius = 200;

        public int ScaleRadius
        {
            get => _scaleRadius;
            set => SetProperty(ref _scaleRadius, value);
        }

        private int _pointerLength = 170;
        public int PointerLength
        {
            get => _pointerLength; 
            set => SetProperty(ref _pointerLength, value);
        }

        private int _rangeIndicatorRadius = 220;
        public int RangeIndicatorRadius
        {
            get => _rangeIndicatorRadius;
            set => SetProperty(ref _rangeIndicatorRadius, value);
        }

        private int _scaleLabelRadius = 150;
        public int ScaleLabelRadius
        {
            get => _scaleLabelRadius; 
            set => SetProperty(ref _scaleLabelRadius, value);
        }

        public int Co2Ppm
        {
            get => _co2Ppm;
            set => SetProperty(ref _co2Ppm, value);
        }

        public MainViewModel() 
        {
            _sensors = new SensorReader("COM13", 9600);
            _sensors.NewValuesAvailableEvent += NewValuesAvailable;
        }

        private void NewValuesAvailable(object? sender, DataPacket packet)
        {
            Co2Ppm = (int)packet.Co2PPM;
        }

        public void WindowSizeChanged(System.Windows.Size newSize)
        {
            var fullSize = (int)Math.Min(newSize.Width, newSize.Height);

            Radius = ScaleToSize(fullSize, 0.166666666666667);
            ScaleRadius = ScaleToSize(fullSize, 0.133333333333333);
            PointerLength = ScaleToSize(fullSize, 0.113333333333333);
            RangeIndicatorRadius = ScaleToSize(fullSize, 0.146666666666667);
            ScaleLabelRadius = ScaleToSize(fullSize, 0.1);
        }

        private int ScaleToSize(int fullSize, double scale) => (int)(fullSize * scale);

        [RelayCommand]
        public void ChangeValue()
        {
            Co2Ppm = 500;
        }
    }
}
