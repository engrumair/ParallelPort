using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ParrallelPortDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Writing and reading single byte from Parrallel Port");

            ParallelPort aPort = new ParallelPort();

            aPort.OpenEcpPort();

            Console.WriteLine("Writing a single byte to parralel Port");
            aPort.WriteData(0x37);

            Thread.Sleep(100);

            Console.WriteLine("Read the data from parallel Port");

            if (aPort.TestFifoEmpty() != 1)
            {
                byte data = aPort.ReceiveData();
                Console.WriteLine("Write Data to Console" + data.ToString());

            }
        

            Console.ReadKey();



        }
    }
}
