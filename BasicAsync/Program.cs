using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Runtime.Remoting.Messaging;

namespace BasicAsync
{
    class Program
    {
        public delegate int BinaryOp(int x, int y );

        static void Main(string[] args)
        {
           //  SyncDelegate();

            AyncDelegate();

         

            Console.Read();
        }

        static void MethodonThread()
        { }

        static int Add(int x, int y)
        {
            Console.WriteLine("Add() invoked ON thread ID " + Thread.CurrentThread.ManagedThreadId);

            Thread.Sleep(4000);

            return (x + y);
        }

        static void SyncDelegate()
        {
            Console.WriteLine("******* Sync Delegate review *********");

            Console.WriteLine("ON thread ID " + Thread.CurrentThread.ManagedThreadId );

            BinaryOp op = new BinaryOp(Add);

            int result = op(10, 10);
            Console.WriteLine("Doing more work on main");
            Console.WriteLine(string.Format("Result of 1 + 10 ={0}", result));
            
        }

        static void AyncDelegate()
        {
            Console.WriteLine("******* ASync Delegate review *********");

            Console.WriteLine("ON thread ID " + Thread.CurrentThread.ManagedThreadId);

            BinaryOp op = new BinaryOp(Add);

              op.BeginInvoke(10, 10, AsyncCallbackMethod, null);
             
            Console.WriteLine("Doing more work on main");
           
        }

        static void AsyncCallbackMethod(IAsyncResult result)
        {
            Console.WriteLine("ON thread ID " + Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine("Work complete");

            AsyncResult ar = (AsyncResult)result;
            BinaryOp op = (BinaryOp)ar.AsyncDelegate;
            Console.WriteLine("result : " + op.EndInvoke(ar));
        }


       
    }
}
