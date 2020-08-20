
using System;
using System.Threading;
using System.Threading.Tasks;


class Program
{
    static async Task Main()
    {
        // Call Task.Run and invoke Method1.
        // ... Then call Method2.
        //     Finally wait for Method2 to finish for terminating the program.
       Console.WriteLine("Main ThreadId : "+Thread.CurrentThread.ManagedThreadId);
       var task1= Task.Run(() => Method1());
       Console.WriteLine("Running in parallel");
       var task2= Task.Run(() => Method2());
       Console.WriteLine("Main  ThreadId : "+Thread.CurrentThread.ManagedThreadId);
       await task1;
       Console.WriteLine("Main  ThreadId : "+Thread.CurrentThread.ManagedThreadId);
       await task2;  
       Console.WriteLine("Main  ThreadId : "+Thread.CurrentThread.ManagedThreadId);
    }

    static void Method1()
    {   Console.WriteLine("ThreadId : "+Thread.CurrentThread.ManagedThreadId);
        Thread.Sleep(6000);
        Console.WriteLine("::Method1::");
    }

    static void Method2()
    {
        Console.WriteLine("ThreadId : "+Thread.CurrentThread.ManagedThreadId);
        Thread.Sleep(6000);
        Console.WriteLine("::Method2::");
    }
}