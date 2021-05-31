﻿using System;
using System.Diagnostics;
using System.Threading;

namespace ZenMind.SharedLib.Tools
{
    public static class Retry
    {
        public static void WhileTrue(Func<bool> methodToCheck, int timeoutInSeconds = 2, int sleepInMs = 100)
        {
            var watch = new Stopwatch();
            watch.Start();
            while ( methodToCheck() && watch.ElapsedMilliseconds < timeoutInSeconds*1000)
            {
                Thread.Sleep(sleepInMs);
            }
            watch.Stop();
        }        
        public static void WhileFalse(Func<bool> methodToCheck, int timeoutInSeconds = 2, int sleepInMs = 100)
        {
            var watch = new Stopwatch();
            watch.Start();
            while (!methodToCheck() && watch.ElapsedMilliseconds < timeoutInSeconds * 1000)
            {
                Thread.Sleep(sleepInMs);
            }
            watch.Stop();
        }

    }
}