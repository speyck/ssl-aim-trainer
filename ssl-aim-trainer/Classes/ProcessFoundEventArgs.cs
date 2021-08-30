using System;

namespace ssl_aim_trainer.Classes
{
    public class ProcessFoundEventArgs : EventArgs
    {
        public readonly string ProcessID;
        public readonly DateTime FoundTime;

        public ProcessFoundEventArgs(string process, DateTime foundTime)
        {
            ProcessID = process;
            FoundTime = foundTime;
        }
    }
}