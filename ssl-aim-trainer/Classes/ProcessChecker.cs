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
        public delegate void ProcessFoundEventHandler(object sender, ProcessCheckerEventArgs e);
        public delegate void ProcessClosedEventHandler(object sender, ProcessCheckerEventArgs e);

        /// <summary>
        /// This event is raised when the provided process was found
        /// </summary>
        public event ProcessFoundEventHandler ProcessFound;

        /// <summary>
        /// If the process was found and then later closed again, this event is raised
        /// </summary>
        public event ProcessClosedEventHandler ProcessClosed;

        private readonly BackgroundWorker Worker = new()
        {
            WorkerSupportsCancellation = true
        };

        private readonly Mem Memory;

        public readonly string ProcessID;

        public readonly int Interval;

        /// <summary>
        /// Indicates whether the process is running
        /// </summary>
        public bool ProcessRunning { get; private set; } = false;

        /// <summary>
        /// Creates a new ProcessChecker object
        /// </summary>
        /// <param name="processID">String of the process id (.exe is not needed)</param>
        /// <param name="memory">Optional Mem object</param>
        public ProcessChecker(string processID, Mem memory, int interval = 1000)
        {
            ProcessID = processID;
            Interval = interval;
            Memory = memory ?? new();

            Worker.DoWork += Worker_DoWork;
        }

        /// <summary>
        /// Starts the Worker searching for the provided process
        /// </summary>
        public void Start() => Worker.RunWorkerAsync();

        /// <summary>
        /// Stops the Worker checking the process
        /// </summary>
        public void Stop() => Worker.CancelAsync();

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                // Set corrent state
                bool running = Memory.OpenProcess(ProcessID);

                // Check if running state has changed
                if (running == ProcessRunning)
                {
                    continue;
                }

                ProcessRunning = running;

                if (ProcessRunning)
                {
                    // Raise event. '?.' is so that the event is only raised when it's subscribed, otherwise it would throw an exception
                    ProcessFound?.Invoke(this, new ProcessCheckerEventArgs(ProcessID, DateTime.Now));
                }
                else if(!ProcessRunning)
                {
                    // Raise event. '?.' is so that the event is only raised when it's subscribed, otherwise it would throw an exception
                    ProcessClosed?.Invoke(this, new ProcessCheckerEventArgs(ProcessID, DateTime.Now));
                }

                Thread.Sleep(Interval);
            }
        }
    }
}
