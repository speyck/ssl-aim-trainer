using System;
using System.ComponentModel;
using System.Threading;
using Memory;

namespace ssl_aim_trainer.Classes
{
    /// <summary>
    /// Checks if a provided process, then raises an event
    /// </summary>
    public class ProcessChecker
    {
        public delegate void ProcessFoundEventHandler(object sender, EventArgs e);

        /// <summary>
        /// This event is raised when the provided process was found
        /// </summary>
        public event ProcessFoundEventHandler ProcessFound;

        private readonly BackgroundWorker Worker = new();

        private readonly Mem Memory;

        public readonly string ProcessID;

        public readonly int Interval;

        /// <summary>
        /// Creates a new ProcessChecker object
        /// </summary>
        /// <param name="processID">String of the process id (.exe is not needed)</param>
        /// <param name="memory">Optional Mem object</param>
        public ProcessChecker(string processID, int interval = 1000, Mem memory = null)
        {
            ProcessID = processID;
            Interval = interval;
            Memory = memory ?? new();

            Worker.DoWork += Worker_DoWork;
        }

        /// <summary>
        /// Starts the Worker searching for the provided process
        /// </summary>
        public void RunWorker() => Worker.RunWorkerAsync();

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                if (Memory.OpenProcess(ProcessID))
                {
                    // Raise event. '?.' is so that the event is only raised when it's subscribed, otherwise it would throw an exception
                    ProcessFound?.Invoke(this, new EventArgs());

                    return;
                }

                Thread.Sleep(Interval);
            }
        }
    }
}
