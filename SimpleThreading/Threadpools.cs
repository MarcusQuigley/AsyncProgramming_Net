using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleThreading
{
    static class Threadpools
    {
        internal static void Program()
        {
            TasksWaitAny();
        }

        //threadpool are managed
        internal static void MainThreadpools()
        {
            ThreadPool.QueueUserWorkItem((s) =>
                {
                    Console.WriteLine("Working on threadpool");
                });

            Console.ReadLine();
        }
        //tasks uses threadpool
        public static void TaskMethod()
        {
            //Task t = Task.Factory.StartNew(ThreadMethod);
            //t.Wait();

            Task<int> tReturn = Task<int>.Factory.StartNew(() =>
            {
                return 42;
            }).ContinueWith((i) =>
                {
                    return (i.Result * 2);
                });

            tReturn = tReturn.ContinueWith((i) =>
                {
                    return (i.Result * 2);
                });

            Console.WriteLine(tReturn.Result);
            Console.ReadLine();
        }

        public static void ThreadMethod()
        {
            for (int i = 0; i < 100; i++)
            {
                Console.Write("*");
            }
        }

        private static void TaskMethod2()
        {
            Task<int> t = Task<int>.Factory.StartNew(() =>
                {
                   // if (new Random().Next(4) == 2) throw new Exception("Exception in task 1");
                    return 2;
                }).ContinueWith((i) =>
            {
               // if (new Random().Next(4) == 2) throw new Exception("Exception in task 2");
                return (i.Result * 2);
            }).ContinueWith<int>((i) =>
                {
              //      if (new Random().Next(4) == 2) throw new Exception("Exception in task 3");
                    return (i.Result * 2);
                });
            t = t.ContinueWith<int>((j) =>
                {
              //      if (new Random().Next(4) == 2) throw new Exception("Exception in task 4");
                    return (j.Result * 2);   
                });

            t.ContinueWith((i) =>
                {
                    Console.WriteLine("Finishing up now lets return the sq of {0}", Math.Sqrt((double)i.Result));
                    return (Math.Sqrt((double)i.Result));
                }, TaskContinuationOptions.OnlyOnRanToCompletion);

            Console.WriteLine("No errors..? result={0}",t.Result);
            Console.ReadLine();
        }


        private static void TasksWaitAll()
        {
            Task[] tasks = new Task[3];

            tasks[0] = Task.Factory.StartNew(()=>
            {
                Thread.Sleep(1000);
                Console.WriteLine("1");
                return 1;
            });

            tasks[1] = Task.Factory.StartNew(() =>
            {
                Thread.Sleep(1000);
                Console.WriteLine("2");
                return 1;
            });

            tasks[2] = Task.Factory.StartNew(() =>
            {
                Thread.Sleep(1000);
                Console.WriteLine("3");
                return 1;
            });

            Task.WaitAll(tasks);
            Console.WriteLine("Finished");
            Console.ReadLine();
        }

        private static void TasksWaitAny()
        {
            Task<int>[] tasks = new Task<int>[3];
            tasks[0] = Task.Factory.StartNew(()=>
                {
                    Console.WriteLine("Task 1 is working");
                    Thread.Sleep(4000);
                    return 1;
                });

            tasks[1] = Task.Factory.StartNew<int>(() =>
            {
                Console.WriteLine("Task 2 is working");
                Thread.Sleep(1000);
                return 2;
            });

            tasks[2] = Task.Factory.StartNew<int>(() =>
            {
                Console.WriteLine("Task 3 is working");
                Thread.Sleep(2000);
                return 3;
            });



            while (tasks.Length > 0)
            {
                int i = Task.WaitAny(tasks);
                Task<int> completedTask =tasks[i];

                Console.WriteLine("Task has finished. Result={0}", completedTask.Result);
                var tempList = tasks.ToList();
                tempList.RemoveAt(i);
                tasks = tempList.ToArray();
           }

            Console.WriteLine("All tasks completed");
                Console.ReadLine();
        }
    }
}
