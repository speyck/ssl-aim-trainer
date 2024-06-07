using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;


namespace ssl_aim_trainer.Classes
{
    public class Memory
    {
        #region Kernel32 imports
        [DllImport("kernel32.dll")]
        private static extern bool ReadProcessMemory(IntPtr hProcess, UIntPtr lpBaseAddress, [Out] byte[] lpBuffer, UIntPtr nSize, IntPtr lpNumberOfBytesRead);

        [DllImport("kernel32.dll")]
        private static extern bool ReadProcessMemory(IntPtr hProcess, UIntPtr lpBaseAddress, [Out] byte[] lpBuffer, UIntPtr nSize, out ulong lpNumberOfBytesRead);

        [DllImport("kernel32.dll")]
        private static extern bool ReadProcessMemory(IntPtr hProcess, UIntPtr lpBaseAddress, [Out] IntPtr lpBuffer, UIntPtr nSize, out ulong lpNumberOfBytesRead);

        [DllImport("kernel32.dll")]
        private static extern IntPtr OpenProcess(UInt32 dwDesiredAccess, bool bInheritHandle, Int32 dwProcessId);
        #endregion

        private Process _process;
        public Memory()
        {

        }

        #region OpenProcess
        public static int GetProcessIdFromString(string name)
        {
            // Removes the strings in the list from the name
            new List<string>()
            {
                ".exe",
                ".bin"
            }.ForEach(s => name = name.Contains(s) ? name.Replace(s, "") : name);

            Process process = Process.GetProcesses().FirstOrDefault(p => p.ProcessName.Equals(name, StringComparison.CurrentCultureIgnoreCase));

            return process != default(Process) ? process.Id : 0;
        }

        public static Process GetProcess(int id)
        {
            return Process.GetProcessById(id);
        }

        public static Process GetProcess(string process)
        {
            return GetProcess(GetProcessIdFromString(process));
        }

        public bool OpenProcess(int id)
        {
            try
            {
                _process = Process.GetProcessById(id);
            }
            catch
            {
                return false;
            }

            return !(_process != null && !_process.Responding);
        }

        public bool OpenProcess(string process)
        {
            return OpenProcess(GetProcessIdFromString(process));
        }
        #endregion
    }
}