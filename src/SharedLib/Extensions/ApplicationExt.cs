using System;
using SharedLib.Tools;

namespace SharedLib.Extensions
{
    public static class ApplicationExt
    {
        public static Application WaitWhileMainHandleIsMissing(this Application application, int waitTimeoutInSeconds = 5)
        {
            Retry.WhileTrue(() => application.MainWindowHandle == IntPtr.Zero, waitTimeoutInSeconds);

            return application;
        }
        
    }
}