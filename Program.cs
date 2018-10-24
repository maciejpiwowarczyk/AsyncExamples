using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace EightQueens
{
    class Program
    {
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += OnAppDomainUnhandled;
            TaskScheduler.UnobservedTaskException += OnUnobservedTaskException;

            //new Thread(ContinuousGc).Start();
            Printer.StartMeasuringTime();
            Printer.Print("Start");
            try
            {
                //AsyncExamples.AsyncVoid();
                //AsyncExamples.AsyncTask();
                //AsyncExamples.AsyncTask().GetAwaiter().GetResult();
                //AsyncExamples.AsyncTask().InAsyncSafe();
                //AsyncExamples.AsyncTaskWithOwnTryCatch();
                //AsyncExamples.AsyncTaskWithOwnTryCatchNested();
                //AsyncExamples.ExecutionOrderFirst().InAsyncSafe();
                //AsyncExamples.ThrowableComputationFlowAsync(new Exception("do not suppress"), ex => ex.Message == "suppress").InAsyncSafe();
                //AsyncExamples.ThrowableComputationFlowAsync(new Exception("suppress"), ex => ex.Message == "suppress").InAsyncSafe();
                //AsyncExamples.ThrowableComputationFlowWithDefaultSuppressAsync(new Exception("do not suppress")).InAsyncSafe();
                AsyncExamples.ThrowableComputationFlowWithDefaultSuppressAsync(new Exception("suppress")).InAsyncSafe();

                Printer.Print("Press any key...");
                Console.Read();
            } 
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            Printer.Print("Done!");
        }

        private static void OnAppDomainUnhandled(object sender, UnhandledExceptionEventArgs e)
        {
            Printer.Print($"AppDomain.Unhandled exception: {e.ExceptionObject}");
        }

        private static void OnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            Printer.Print($"Task.OnUnobservedTaskException exception: {e.Exception}");
        }

        private static void ContinuousGc()
        {
            while (true)
            {
                Printer.Print("Executing full GC...");
                GC.Collect(2);
                GC.WaitForPendingFinalizers();
                GC.Collect(2);
                Thread.Sleep(5000);
            }
        }
    }
}
