using System;

namespace ssl_aim_trainer.Classes
{
    public class MemoryTypeNotSupportedException : Exception
    {
        public readonly string Type;

        public MemoryTypeNotSupportedException(string type)
        {
            Type = type;
        }
    }
}
