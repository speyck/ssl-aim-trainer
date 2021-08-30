using System;

namespace ssl_aim_trainer.Classes
{
    public class GotAddressValueEventArgs : EventArgs
    {
        public readonly object Value;

        public readonly MemoryReadType ReadType;

        public GotAddressValueEventArgs(object value, MemoryReadType readType)
        {
            Value = value;
            ReadType = readType;
        }
    }
}
