using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using Memory;

namespace ssl_aim_trainer.Classes
{
    public class PointerAddressReader
    {
        private readonly BackgroundWorker Worker = new()
        {
            WorkerSupportsCancellation = true
        };

        private readonly Mem m;

        /// <summary>
        /// EventHandler for the GotAddressValue Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void GotAddressValueHandler(object sender, GotAddressValueEventArgs e);

        /// <summary>
        /// Gets raised when a address is read (depending on the given interval)
        /// </summary>
        public event GotAddressValueHandler GotAddressValue;       

        /// <summary>
        /// The address that is being read off
        /// </summary>
        public readonly string Address;

        /// <summary>
        /// What type is being read
        /// </summary>
        public readonly string Type;

        /// <summary>
        /// With what delay should the address be read
        /// </summary>
        public readonly int Interval;

        public readonly MemoryReadType ReadType;

        /// <summary>
        /// Creates a new PointerAddressReader object
        /// </summary>
        /// <param name="address">What address should be read</param>
        /// <param name="type">What type should be read</param>
        /// <param name="interval">What interval the reader should have in ms</param>
        /// <param name="m">Mem object with an open process</param>
        public PointerAddressReader(string address, string type, int interval, Mem m)
        {
            ReadType = ReadTypes.FirstOrDefault(t => t.ReadType == type);

            if (ReadType == default(MemoryReadType))
            {
                throw new MemoryTypeNotSupportedException(type);
            }

            this.m = m;

            Address = address;
            Type = type;
            Interval = interval;

            Worker.DoWork += Worker_DoWork;
        }

        /// <summary>
        /// Starts the BackgroundWorker
        /// </summary>
        public void Start() => Worker.RunWorkerAsync();

        /// <summary>
        /// Stops the BackgroundWorker
        /// </summary>
        public void Stop() => Worker.CancelAsync();

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                object value = ReadMemory(m, Type, Address);

                GotAddressValue?.Invoke(this, new(value, ReadType));

                Thread.Sleep(Interval);
            }
        }

        /// <summary>
        /// Reads the value of the provided address and returns it. Attention: Not all types are compatible. If the type is not compatible a AddressTypeNotSupportedException is thrown
        /// </summary>
        /// <param name="m">Mem object with open process to read memory</param>
        /// <param name="type">What type should be read</param>
        /// <param name="address">What address should be read</param>
        /// <returns>The read value from the address</returns>
        public static object ReadMemory(Mem m, string type, string address, string file = "")
        {
            MemoryReadType readType = ReadTypes.FirstOrDefault(t => t.ReadType == type);

            return readType == default(MemoryReadType) ? throw new MemoryTypeNotSupportedException(type) : readType.ReadMemory(m, address, file);
        }

        /// <summary>
        /// Same as ReadMemory method but will return the value in the given type T. Use this if you know what type is returned, otherwise use the non-generic method.
        /// </summary>
        /// <typeparam name="T">What value the returned type is (must be correct, otherwise a exception is thrown!)</typeparam>
        /// <param name="m">Mem object with open process to read memory</param>
        /// <param name="type">What type should be read</param>
        /// <param name="address">What address should be read</param>
        /// <returns>The read value from the address as a T object</returns>
        public static T ReadMemory<T>(Mem m, string type, string address, string file = "") => (T)ReadMemory(m, type, address, file);

        /// <summary>
        /// Contains all normal readable types from a Mem object. The readType is what value is read and the returnType is what is being returned. You need to convert the result of ReadMemory to the right type.
        /// </summary>
        public static readonly IEnumerable<MemoryReadType> ReadTypes = new List<MemoryReadType>()
        {
            new ("2byte",   typeof(int),    (m, code, file) => m.Read2Byte(code, file)),
            new ("bits",    typeof(bool[]), (m, code, file) => m.ReadBits(code, file)),
            new ("byte",    typeof(int),    (m, code, file) => m.ReadByte(code, file)),
            new ("double",  typeof(double), (m, code, file) => m.ReadDouble(code, file)),
            new ("float",   typeof(float),  (m, code, file) => m.ReadFloat(code, file)),
            new ("int",     typeof(int),    (m, code, file) => m.ReadInt(code, file)),
            new ("long",    typeof(long),   (m, code, file) => m.ReadLong(code, file)),
            new ("uint",    typeof(uint),   (m, code, file) => m.ReadUInt(code, file)),
            new ("string",  typeof(string), (m, code, file) => m.ReadString(code, file))
        };
    }
}
