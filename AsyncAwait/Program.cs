using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace AsyncProgramming
{
    class Program
    {
        static async Task Main(string[] args)
        {
            
            Console.WriteLine("Main Method ThreadId : "+Thread.CurrentThread.ManagedThreadId);    
            Console.WriteLine("Main Method ThreadId : "+Thread.CurrentThread.ManagedThreadId);
            Coffee cup = PourCoffee();
            Console.WriteLine("Main Method post pourcoffeeSync ThreadId : "+Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine("Main Method going to fry eggsasync ThreadId : "+Thread.CurrentThread.ManagedThreadId);
            var eggsTask = FryEggsAsync(2);
            Console.WriteLine("Main Method post fry eggs ThreadId : "+Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine("Main Method going to fry baconaAync ThreadId : "+Thread.CurrentThread.ManagedThreadId);
            var baconTask = FryBaconAsync(3);
             Console.WriteLine("Main Method post fry baconAsync ThreadId : "+Thread.CurrentThread.ManagedThreadId);
            var toastTask = MakeToastWithButterAndJamAsync(2);

            var breakfastTasks = new List<Task> { eggsTask, baconTask, toastTask };
            while (breakfastTasks.Count > 0)
            {
                Console.WriteLine("Main Method going to await task ThreadId : "+Thread.CurrentThread.ManagedThreadId);
                Task finishedTask = await Task.WhenAny(breakfastTasks);
                if (finishedTask == eggsTask)
                {
                    Console.WriteLine("eggs are ready");
                }
                else if (finishedTask == baconTask)
                {
                    Console.WriteLine("bacon is ready");
                }
                else if (finishedTask == toastTask)
                {
                    Console.WriteLine("toast is ready");
                }
                breakfastTasks.Remove(finishedTask);
                Console.WriteLine("Main Method await task completed ThreadId : "+Thread.CurrentThread.ManagedThreadId);

            }
            Console.WriteLine("Main Method going to call PourOJSync ThreadId : "+Thread.CurrentThread.ManagedThreadId);
            Juice oj = PourOJ();
            Console.WriteLine("Main Method PourOJSync completed ThreadId : "+Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine("oj is ready");
            Console.WriteLine("Breakfast is ready!");
        }

        static async Task<Toast> MakeToastWithButterAndJamAsync(int number)
        {
            if(SynchronizationContext.Current==null)
            {
                Console.WriteLine("null sync context "+System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            Console.WriteLine("starting MakeToastWithButterAndJamAsync ThreadId : "+Thread.CurrentThread.ManagedThreadId);
            var toast = await ToastBreadAsync(number);
            ApplyButter(toast);
            ApplyJam(toast);
             Console.WriteLine("ending MakeToastWithButterAndJamAsync ThreadId : "+Thread.CurrentThread.ManagedThreadId);
            return toast;
        }

        private static Juice PourOJ()
        {
            Console.WriteLine("starting PourOJ Sync ThreadId : "+Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine("Pouring orange juice");
            Console.WriteLine("ending PourOJ Sync ThreadId : "+Thread.CurrentThread.ManagedThreadId);
            return new Juice();
        }

        private static void ApplyJam(Toast toast) {
         Console.WriteLine("starting ApplyJam Sync ThreadId : "+Thread.CurrentThread.ManagedThreadId);
         Console.WriteLine("Putting jam on the toast");
         Console.WriteLine("ending ApplyJam Sync ThreadId : "+Thread.CurrentThread.ManagedThreadId);
        }
        private static void ApplyButter(Toast toast) {
          Console.WriteLine("starting ApplyButter Sync ThreadId : "+Thread.CurrentThread.ManagedThreadId);
          Console.WriteLine("Putting butter on the toast");
          Console.WriteLine("ending ApplyButter Sync ThreadId : "+Thread.CurrentThread.ManagedThreadId);

        }
        private static async Task<Toast> ToastBreadAsync(int slices)
        {
            Console.WriteLine("starting ToastBreadAsync ThreadId : "+Thread.CurrentThread.ManagedThreadId);
             for (int slice = 0; slice < slices; slice++)
            {
                Console.WriteLine("Putting a slice of bread in the toaster");
            }
            Console.WriteLine("Start toasting...");
            await Task.Delay(3000);
            Console.WriteLine("Remove toast from toaster");
            Console.WriteLine("ending ToastBreadAsync ThreadId : "+Thread.CurrentThread.ManagedThreadId);
            return new Toast();
        }

        private static async Task<Bacon> FryBaconAsync(int slices)
        {
            Console.WriteLine("starting FryBaconAsync ThreadId : "+Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine($"putting {slices} slices of bacon in the pan");
            Console.WriteLine("cooking first side of bacon...");
            await Task.Delay(3000);
            Console.WriteLine("ThreadId : "+Thread.CurrentThread.ManagedThreadId);
            for (int slice = 0; slice < slices; slice++)
            {
                Console.WriteLine("flipping a slice of bacon");
            }
            Console.WriteLine("cooking the second side of bacon...");
            await Task.Delay(3000);
            Console.WriteLine("Put bacon on plate");
            Console.WriteLine("ending FryBaconAsync ThreadId : "+Thread.CurrentThread.ManagedThreadId);
            return new Bacon();
        }

        private static async Task<Egg> FryEggsAsync(int howMany)
        {
            Console.WriteLine("starting FryEggsAsync ThreadId : "+Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine("Warming the egg pan...");
            await Task.Delay(3000);
            Console.WriteLine("FryEggsAsync breaking eggs ThreadId : "+Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine($"cracking {howMany} eggs");
            Console.WriteLine("cooking the eggs ...");
            await Task.Delay(3000);
            Console.WriteLine("Put eggs on plate");
            Console.WriteLine("ending FryEggsAsync ThreadId : "+Thread.CurrentThread.ManagedThreadId);
            return new Egg();
        }

        private static Coffee PourCoffee()
        {
           Console.WriteLine("starting PourCoffee ThreadId : "+Thread.CurrentThread.ManagedThreadId);
           Console.WriteLine("Pouring coffee");
           Console.WriteLine("ending PourCoffee ThreadId : "+Thread.CurrentThread.ManagedThreadId);
            return new Coffee();
        }
 
    }

   
}
