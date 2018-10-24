using System;
using System.Threading.Tasks;

public static class AsyncExamples
{
    public static async void AsyncVoid()
    {
        Printer.Print("Starting...");
        await Task.Delay(TimeSpan.FromSeconds(1));
        Printer.Print("Finished and throwing...");
        throw new Exception("_boom: AsyncVoid_");
    }

    public static async Task AsyncTask()
    {
        Printer.Print("Starting...");
        await Task.Delay(TimeSpan.FromSeconds(1));
        Printer.Print("Finished and throwing...");
        throw new Exception("_boom: AsyncTask_");
    }

    public static void InAsyncSafe(this Task task)
    {
        task.ContinueWith(t => {
            Printer.Print($"Caught InAsyncSafe: {t.Exception}");
        }, TaskContinuationOptions.OnlyOnFaulted);
    }

    public static async Task AsyncTaskWithOwnTryCatch()
    {
        try
        {
            Printer.Print("Starting...");
            await Task.Delay(TimeSpan.FromSeconds(1));
            Printer.Print("Finished and throwing...");
            throw new Exception("_boom: AsyncTaskWithOwnTryCatch_");
        }
        catch (Exception ex)
        {
            Printer.Print($"Caught exception: {ex}");
        }
    }

    public static async Task AsyncTaskWithOwnTryCatchNested()
    {
        try
        {
            Printer.Print("Starting...");
            await NestedAsyncCall();
            Printer.Print("Finished and throwing...");
            throw new Exception("_boom: AsyncTaskWithOwnTryCatchNested_");
        }
        catch (Exception ex)
        {
            Printer.Print($"Caught exception: {ex}");
        }
    }

    private static async Task NestedAsyncCall()
    {
        Printer.Print("Starting...");
        await Task.Delay(TimeSpan.FromSeconds(1));
        Printer.Print("Finished and throwing...");
        throw new Exception("_boom: NestedAsyncCall_");
    }
}