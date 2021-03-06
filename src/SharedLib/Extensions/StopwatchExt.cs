using System.Diagnostics;

namespace ZenMind.SharedLib.Extensions
{
    public static class StopwatchExt
    {
        public static long ElapsedSeconds(this Stopwatch stopwatch, int timeoutInSeconds = 5) =>
            stopwatch.ElapsedMilliseconds / 1000;
    }
}