using System;
using System.Diagnostics;
using System.Threading;
using System.Runtime.CompilerServices;

static class Printer
{
    private static Stopwatch sw = new Stopwatch();

    public static void StartMeasuringTime()
    {
        sw.Start();
    }

    public static void Print(string message, [CallerMemberName] string memberName = "")
    {        
        Console.WriteLine($"{sw.ElapsedMilliseconds,5} [{Thread.CurrentThread.ManagedThreadId}] {memberName}: " + message);
    }
}