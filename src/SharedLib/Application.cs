using System;
using System.ComponentModel;
using System.Diagnostics;
using Ardalis.GuardClauses;

namespace SharedLib
{
    public class Application
    {
        private readonly Process _process;

        private Application(Process process) => _process = process ?? throw new ArgumentNullException(nameof (process));

        public IntPtr MainWindowHandle => Process.GetProcessById(_process.Id).MainWindowHandle;
        
        public static Application Launch(string path, string arguments = "")
        {
            Guard.Against.NullOrEmpty(path, nameof(path));
            Guard.Against.Null(arguments, nameof(arguments));
            
            return Launch(new ProcessStartInfo(path, arguments) {UseShellExecute = true});
        }

        public static Process FindProcess(int processId)
        {
            try
            {
                return Process.GetProcessById(processId);
            }
            catch (Exception ex)
            {
                throw new Exception("Could not find process with id: " + processId, ex);
            }
        }

        public void Close()
        {
            try
            {
                _process.Kill();
                _process.WaitForExit();
            }
            catch { }
        }

        private static Application Launch(ProcessStartInfo startInfo)
        {
            try
            {
                return new Application(Process.Start(startInfo));
            }
            catch (Win32Exception e)
            {
                Log("Failed to launch process.", e);
                throw;
            }
        }

        private static void Log(string message, Exception ex) => Console.Out.WriteLine(message + " Exception: " + ex.Message);
    }
}