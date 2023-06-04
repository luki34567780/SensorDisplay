using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SensorDisplay
{
    [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi,Pack=1)]
    internal struct DataPacket 
    {
        public const int Size = sizeof(double) * 5;
        public double Co2PPM;
        public double TemperatureSensor;
        public double Humidity;
        public double TVOC;
        public double CpuTemperatureSensor;
    }
}
