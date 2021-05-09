using System;
using System.Diagnostics;
using System.Linq;
using System.Management;
using Ardalis.GuardClauses;
using static PInvoke.User32;
using static SharedLib.SystemApi.DwmApi;

namespace SharedLib.SystemApi
{
    /// <summary>
    /// Wrapper for ApplicationWindows API calls (User32, DWMApi etc.)
    /// </summary>
    internal sealed class WindowApi
    {
        public WindowApi(IntPtr handle) => Handle = Guard.Against.Default(handle, nameof(handle));

        public IntPtr Handle { get; }
        public string Title => GetWindowText(Handle);
        public bool IsVisible => IsWindowVisible(Handle);
        public string ClassName => GetClassName(Handle);
        public WindowStyles Styles => new WindowStyles(Handle);
        public string ProcessName => Process.GetProcessById(ProcessId).ProcessName.Trim();
        public int ProcessId => GetProcessId();

        public bool IsCloaked
        {
            get
            {
                
                DwmGetWindowAttribute(Handle, DWMWA_CLOAKED, out var cloaked, sizeof(int));
                return cloaked != 0;
            }
        }

        public string GetCommandLine()
        {
            var query = $"SELECT CommandLine FROM Win32_Process WHERE ProcessId = {ProcessId}";

            using var searcher = new ManagementObjectSearcher(query);
            using var result = searcher.Get();
            return result.Cast<ManagementBaseObject>().SingleOrDefault()?["CommandLine"]?.ToString() ?? "";
        }

        private int GetProcessId()
        {
            GetWindowThreadProcessId(Handle, out var processId);
            return processId;
        }
    }
}