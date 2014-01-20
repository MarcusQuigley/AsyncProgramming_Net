using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SimpleThreading
{
    //block a thread until you signal to stop blocking!
   static class ManualResetEventClass
    {
     
      public static void Program()
      {
              ManualResetEvent _signalViolin = new ManualResetEvent(false);
        ManualResetEvent _signalTrumpet = new ManualResetEvent(false);

          Thread tPlayViolin = new Thread(() =>
          {
              _signalViolin.WaitOne();
              for (int i = 0; i < 10; i++)
              {
                   Console.WriteLine(Thread.CurrentThread.Name + " " + i);
                   Thread.Sleep(200);
               }
          });
          tPlayViolin.Name = "Violins";
          tPlayViolin.Start();

          Thread tPlayTrumpet = new Thread(() =>
          {
              _signalTrumpet.WaitOne();
              for (int i = 0; i < 10; i++)
              {
                  Console.WriteLine(Thread.CurrentThread.Name + " " + i);
                  Thread.Sleep(200);
              }
          });
          tPlayTrumpet.Name = "Trumpets";
          tPlayTrumpet.Start();

          Console.WriteLine("Any key to start playing\n.");
          Console.ReadKey();
          _signalViolin.Set();
          Console.ReadKey();
          _signalTrumpet.Set();

          tPlayViolin.Join();
          Console.WriteLine("\nFinished playing.");

          Console.Read();
      }

       //static void PlayViolin(int i)
       //{

       //    Console.WriteLine("Playing violin {0}", i);
       //    Thread.Sleep(500);
       //    _signalViolin.WaitOne();
       //}

       

    }
}
