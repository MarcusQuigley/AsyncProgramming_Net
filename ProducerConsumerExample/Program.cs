using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProducerConsumerExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Using PCQueue\n");
            using (ProducerConsumerQueue pcQ = new ProducerConsumerQueue())
            {
                pcQ.Enqueue("Lets Start by counting to 3");
                pcQ.Enqueue("1");
                pcQ.Enqueue("2");
                pcQ.Enqueue(null);
                pcQ.Enqueue("3");
            }

            Console.WriteLine("Enter to finish");
            Console.Read();
        }
    }
}
