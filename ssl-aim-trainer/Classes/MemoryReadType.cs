using Memory;
using System;

namespace ssl_aim_trainer.Classes
{
    public class MemoryReadType
    {
        public delegate object ReadMethodDelegate(Mem m, string code, string file = "");

        public readonly string ReadType;
        public readonly Type ReturnType;
        public readonly ReadMethodDelegate ReadMethod;

        public MemoryReadType(string readType, Type returnType, ReadMethodDelegate readMethod)
        {
            ReadType = readType;
            ReturnType = returnType;
            ReadMethod = readMethod;
        }

        public object ReadMemory(Mem m, string address, string file = "") => ReadMethod(m, address, file);
    }
}
