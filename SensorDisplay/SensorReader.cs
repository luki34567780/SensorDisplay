using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Timers;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;
using System.Windows;

namespace SensorDisplay
{
    internal class SensorReader
    {
        public delegate void NewValuesAvailable(object? sender, DataPacket packet);
        public event NewValuesAvailable NewValuesAvailableEvent;
        public SerialPort SerialPort { get; private set; }
        public System.Timers.Timer Timer { get; private set; }
        public double Co2PPM => GetLocked(ref _data.Co2PPM, _dataLock);
        public double TemperatureSensor => GetLocked(ref _data.TemperatureSensor, _dataLock);
        public double CpuTemperatureSensor => GetLocked(ref _data.CpuTemperatureSensor, _dataLock);
        public double Humidity => GetLocked(ref _data.Humidity, _dataLock);

        private object _dataLock = new object();
        private DataPacket _data;
        private string test = "";

        public SensorReader(string portName = "COM1", int baudrate = 115200)
        {
            var names = SerialPort.GetPortNames();
            SerialPort = new SerialPort(portName, baudrate, Parity.None, 8, StopBits.One);
            SerialPort.Handshake = Handshake.None;
            SerialPort.RtsEnable = true;
            SerialPort.DtrEnable = true;
            SerialPort.Open();

            Application.Current.Exit += (_, _2) => SerialPort.Dispose();
            Timer = new System.Timers.Timer(100);
            Timer.Elapsed += Timer_Elapsed;

            Timer.Start();
        }

        private static T GetLocked<T>(ref T source, object lockObject) where T: struct
        {
            lock(lockObject)
            {
                return source;
            }
        }

        private void Timer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            int bytes = SerialPort.BytesToRead;

            // check if we can read a Packet
            if (bytes >= DataPacket.Size)
            {
                byte[] buf = new byte[DataPacket.Size];

                if (SerialPort.Read(buf, 0, DataPacket.Size) < DataPacket.Size)
                {
                    Debugger.Log(0, "", "Serial Port doesn't have enough data!\n");
                    return;
                }

                if (buf.Sum(x => x) == 0)
                {
                    Debugger.Log(0, "", "Everything Zero! Something is wrong!\n");
                    return;
                }

                try
                {
                    var str = Encoding.ASCII.GetString(buf);
                    test += str;
                }
                catch { }

                var handle = GCHandle.Alloc(buf, GCHandleType.Pinned);

                lock (_dataLock)
                {
                    _data = Marshal.PtrToStructure<DataPacket>(handle.AddrOfPinnedObject());
                }

                handle.Free();

                NewValuesAvailableEvent?.Invoke(this, _data);
            }
        }
    }
}
