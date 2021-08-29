using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using Memory;

namespace ssl_aim_trainer.Classes
{
    class PointerAddressReader
    {
        public delegate void GotAddressValueHandler(object sender, GotAddressValueEventArgs e);
        public event GotAddressValueHandler GotAddressValue;

        private readonly BackgroundWorker Worker = new();
        private readonly Mem m;

        public readonly string Address;
        public readonly string Type;
        public readonly int Interval;

        public PointerAddressReader(string address, string type, int interval, Mem m = null)
        {
            if (!SupportedReadTypes.Any(t => t.Key == type))
            {
                throw new AddressTypeNotSupportedException();
            }

            this.m = m ?? new();

            Address = address;
            Type = type;
            Interval = interval;

            Worker.DoWork += Worker_DoWork;
        }

        public void RunWorker() => Worker.RunWorkerAsync();

        public void StopWorker() => Worker.CancelAsync();

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                object value = ReadMemory(Type, Address);

                GotAddressValue?.Invoke(this, new(value));

                Thread.Sleep(Interval);
            }
        }

        /// <summary>
        /// Reads the value of the provided address and returns it. Attention: Not all types are compatible. If the type is not compatible a AddressTypeNotSupportedException is thrown
        /// </summary>
        /// <param name="type">The typ that should be read</param>
        /// <param name="address">What address should be read</param>
        /// <returns></returns>
        public static object ReadMemory(string type, string address, Mem m = null)
        {
            m ??= new();

            return type switch
            {
                "2byte" => m.Read2Byte(address),
                "bits" => m.ReadBits(address),
                "byte" => m.ReadByte(address),
                "double" => m.ReadDouble(address),
                "float" => m.ReadFloat(address),
                "long" => m.ReadLong(address),
                "int" => m.ReadInt(address),
                "uint" => m.ReadUInt(address),
                "string" => m.ReadString(address),
                _ => throw new AddressTypeNotSupportedException(),
            };
        }

        /// <summary>
        /// Contains all readable types from a Mem object. The Key (all lowercase) is what value is read and the Value is what is being returned.
        /// </summary>
        public static Dictionary<string, Type> SupportedReadTypes = new()
        {
            ["2byte"] = typeof(int),
            ["bits"] = typeof(bool[]),
            ["byte"] = typeof(int),
            ["double"] = typeof(double),
            ["float"] = typeof(float),
            ["int"] = typeof(int),
            ["long"] = typeof(long),
            ["int"] = typeof(int),
            ["uint"] = typeof(uint),
            ["string"] = typeof(string)
        };
    }
}
