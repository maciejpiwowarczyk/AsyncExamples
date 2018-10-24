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

    public static async Task ExecutionOrderFirst()
    {
        Printer.Print("Starting");
        await Task.Delay(1000);
        Printer.Print("About to await next method");
        await ExecutionOrderSecond();
        Printer.Print("Finished");
    }

    public static async Task ExecutionOrderSecond()
    {
        Printer.Print("Starting");
        await Task.Delay(1000);
        Printer.Print("About to await next method");
        await ExecutionOrderThird();
        Printer.Print("Finished");
    }

    public static async Task ExecutionOrderThird()
    {
        Printer.Print("Starting");
        await Task.Delay(1000);
        Printer.Print("Finished");
    }

    public static async Task<T> SuppressErrors<T>(this Task<T> task, Func<Exception, bool> suppressPredicate)
    {
        T result = default(T);
        try
        {
            result = await task;
        }
        catch (Exception ex)
        {
            if (suppressPredicate(ex))
            {
                Printer.Print($"Suppressed exception {ex}");
            }
            else
            {
                throw;
            }
        }
        return result;
    }

    public static Task<T> SuppressErrorsDefault<T>(this Task<T> task)
    {
        return SuppressErrors(task, ex => ex.Message == "suppress");
    }

    public static async Task ThrowableComputationFlowAsync(Exception exceptionToThrow, Func<Exception, bool> suppressPredicate)
    {
        Printer.Print("Starting");
        var result = await ThrowableComputationAsync(exceptionToThrow).SuppressErrors(suppressPredicate);
        Printer.Print($"Got result: {result}");
    }

    public static async Task ThrowableComputationFlowWithDefaultSuppressAsync(Exception exceptionToThrow)
    {
        Printer.Print("Starting");
        var result = await ThrowableComputationAsync(exceptionToThrow).SuppressErrorsDefault();
        Printer.Print($"Got result: {result}");
    }

    private static async Task<int> ThrowableComputationAsync(Exception exceptionToThrow)
    {
        Printer.Print("Starting");
        await Task.Delay(1000);
        if (exceptionToThrow == null)
        {
            Printer.Print("Returning result");
            return 42;
        }
        else
        {
            Printer.Print("Throwing");
            throw exceptionToThrow;
        }
    }
}