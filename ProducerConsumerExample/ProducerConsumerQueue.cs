using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ProducerConsumerExample
{
    //class ProducerConsumerQueue1 : IDisposable
    //{
    //    AutoResetEvent _signal = new AutoResetEvent(false);
    //    readonly object locker = new object();
    //    Queue<string> _queue;
    //    Thread t = null;

    //    public ProducerConsumerQueue()
    //    {
    //        _queue = new Queue<string>();

    //          t = new Thread(DoWork);
    //        t.Name = "WorkerQueue";

    //        t.Start();

    //    }

    //    ~ProducerConsumerQueue()
    //    {
    //        Console.WriteLine("Finalizing ProducerConsumerQueue");
    //        Thread.Sleep(500);
    //    }

    //    public void Dispose()
    //    {
    //          Enqueue(null);
    //          t.Join();
    //        _signal.Close();
    //        Console.WriteLine("Disposing ProducerConsumerQueue");

    //    }

    //    //add obj to queue and signal set
    //    public void Enqueue(string message)
    //    {
    //        lock (locker)
    //        {
    //            _queue.Enqueue(message);
    //        }
    //        _signal.Set();
    //    }


    //    private void DoWork()
    //    { 
    //    //loop going that checks the queue
    //        // takes whatever off the queue and works on it
    //        // if obj in queue is null then kill loop
    //        //signal wait when nothing in the queue

    //        while (true)
    //        {
    //            //if (_queue.Count > 0)
    //            //{
    //            //    string result = null;
    //            //    lock (locker)
    //            //    {
    //            //        result = _queue.Dequeue();
    //            //    }
    //            //    if (result == null)
    //            //    {
    //            //        return;
    //            //    }
    //            //    else
    //            //    {
    //            //        Console.WriteLine("Working on queue. Result = {0}", result);
    //            //    }
    //            //}
    //            //else
    //            //{
    //            //    _signal.WaitOne();
    //            //}

    //            string result = null;
    //            lock (locker)
    //            {
    //                if (_queue.Count > 0)
    //                {
    //                    result = _queue.Dequeue();
    //                    if (result == null)
    //                    {
    //                        return;
    //                    }
    //                }
    //            }
    //            if (result != null)
    //            {
    //                Console.WriteLine("Working on queue. Result = {0}", result);
    //            }
    //            else
    //            {
    //                _signal.WaitOne();
    //            }


    //        }
    //    }
    //}

    class ProducerConsumerQueue : IDisposable
    {
        AutoResetEvent _signal = new AutoResetEvent(false);
        Thread _workerThread = null;
        readonly object _locker = new object();
        Queue<string> _tasks = new Queue<string>();

        public ProducerConsumerQueue()
        {
            _workerThread = new Thread(Work);
            _workerThread.Name = "WorkerThread";
            _workerThread.Start();
        }

        public void Dispose()
        {
            Enqueue(null);
            _workerThread.Join();
            _signal.Close();
        }

        public void Enqueue(string task)
        {
            lock (_locker)
            {
                _tasks.Enqueue(task);
            }
            _signal.Set();
        }

        void Work()
        {
            while (true)
            {
                string task = null;
                lock (_locker)
                {
                    if (_tasks.Count > 0)
                    {
                        task = _tasks.Dequeue();
                        if (task == null) return;
                    }
                }
                if (task != null)
                {
                    Console.WriteLine("Performing task: " + task);
                    Thread.Sleep(1000); // simulate work...
                }
                else
                    _signal.WaitOne(); // No more tasks - wait for a signal
            }
        }

    }


}
