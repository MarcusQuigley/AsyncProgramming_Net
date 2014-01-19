using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
namespace SimpleThreading
{
    class Program
    {
        static void Main(string[] args)
        {
            //StoppingLoop();

          //  StaticVars();

            Threadpools.Program();

        }

        static void StoppingLoop()
        {
            bool stopped = false;

            Thread t = new Thread(() =>
                {
                    while (!stopped)
                    {
                        Console.WriteLine("Running on {0}....", Thread.CurrentThread.ManagedThreadId);
                        Thread.Sleep(700);
                    }
                    Console.WriteLine("Exiting thread");
                });

            t.Start();


            Console.WriteLine("Enter key to quit and other to stop process");
            Console.ReadKey();
            stopped = true;

            t.Join();
            Thread.Sleep(1000);

        }
        //[ThreadStatic]
        public static int _field;
        public static volatile object lockObject = new object();
        static void StaticVars()
        {
            new Thread(() =>
                {


                    while (_field < 10)
                    {
                        lock (lockObject)
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("field {0} on threadID {1}", _field, Thread.CurrentThread.ManagedThreadId);
                            _field++;
                            Thread.Sleep(50);
                        }
                    }

                }).Start();

            new Thread(delegate()
               {

                   while (_field < 10)
                   {
                       lock (lockObject)
                       {
                           Console.ForegroundColor = ConsoleColor.Red;
                           Console.WriteLine("field {0} on threadID {1}", _field, Thread.CurrentThread.ManagedThreadId);
                           _field++;
                           Thread.Sleep(50);
                       }
                   }

               }).Start();

            Console.WriteLine("Enter key to quit and other to stop process");
            Console.Read();
        }

    }
}
