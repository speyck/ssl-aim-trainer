using System;

namespace ssl_aim_trainer.Classes
{
    public class ProcessCheckerEventArgs : EventArgs
    {
        public readonly string ProcessID;
        public readonly DateTime FoundTime;

        public ProcessCheckerEventArgs(string process, DateTime foundTime)
        {
            ProcessID = process;
            FoundTime = foundTime;
        }
    }
}