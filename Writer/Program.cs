using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Writer
{
    class Program
    {
        static void Main(string[] args)
        {
            ChannelFactory<IWriter> proxy = new ChannelFactory<IWriter>(new NetTcpBinding(),
           new EndpointAddress("net.tcp://localhost:4000/IWriter"));

            IWriter channel = proxy.CreateChannel();

            WriterImplement wi = new WriterImplement();

            while (true)
            {
                wi.SendItem();
                Console.WriteLine("Poslao podatke");
                Thread.Sleep(2000);

            }

            Console.WriteLine("Writer pokrenut");
            Console.ReadKey();
        }
    }
}
