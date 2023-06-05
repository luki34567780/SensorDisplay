using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SensorDisplay.SensorModels.SensorViewModel
{
    [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi,Pack=1)]
    internal struct DataPacket 
    {
        public const int Size = sizeof(double) * 7 + sizeof(byte);

        public byte AvailabilityData;
        public bool Co2PPMAvailable => GetBit(0);
        public double Co2PPM;
        public bool TemperatureSensorAvailable => GetBit(1);
        public double TemperatureSensor;
        public bool HumidityAvailable => GetBit(2);
        public double Humidity;
        public bool TVOCAvailable => GetBit(3);
        public double TVOC;
        public bool CpuTemperatureSensorAvailable => GetBit(4);
        public double CpuTemperatureSensor;
        public bool CpuFrequencyAvailable => GetBit(5);
        public double CpuFrequencyMhz;
        public bool IdleTimeAvailable => GetBit(6);
        public double IdleTimeMs;

        private bool GetBit(int bitIndex)
        {
            // Shift the byte to the right by the bit index,
            // and perform bitwise AND with 1 to check if the bit is set.
            return ((AvailabilityData >> bitIndex) & 1) == 1;
        }
    }
}
