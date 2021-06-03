using Contracts;
using Contracts.Resources;
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
        //reference za threadovanje: https://docs.microsoft.com/en-us/dotnet/api/system.threading.thread?view=net-5.0 

        public static void ThreadZaSlanje()
        {
            ChannelFactory<IWriter> proxy = new ChannelFactory<IWriter>(new NetTcpBinding(),
              new EndpointAddress("net.tcp://localhost:4000/IWriter"));

            IWriter channel = proxy.CreateChannel();
            Random rand = new Random();
            Random rand1 = new Random();
            Console.WriteLine("Writer thread 1 pokrenut");

            channel.InitList(); //inicijalizujem  descriptione i listu descriptiona

            while (true)
            {
                Codes code = (Codes)(rand.Next(1, 8));
                double value = Math.Round((rand1.NextDouble() * 1000), 2);
                channel.WriterToLB(code, value);
                Thread.Sleep(2000);
            }

        }

        public static void ThreadZaGasenjeIPaljenje()
        {
            ChannelFactory<IWriter> proxy = new ChannelFactory<IWriter>(new NetTcpBinding(),
            new EndpointAddress("net.tcp://localhost:4000/IWriter"));

            IWriter channel = proxy.CreateChannel();
            Thread.Sleep(0);
            Console.WriteLine("Writer thread 2 pokrenut");
            int res;
            do
            {
                res = 0;
                Console.WriteLine("1. Upali workera");
                Console.WriteLine("2. Ugasi workera");
                Console.WriteLine("0. Ugasi app");
                Thread.Sleep(0);
                res = int.Parse(Console.ReadLine());
                if (res == 1)
                {
                    try
                    {
                        channel.TurnOnWorker();
                    }
                    catch (FaultException<CustomException> e)
                    {
                        Console.WriteLine(e.Detail.Message);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                else if (res == 2)
                {
                    try
                    {
                        channel.TurnOffWorker();
                    }
                    catch (FaultException<CustomException> e)
                    {
                        Console.WriteLine(e.Detail.Message);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }

            } while (res > 0);

        }


        static void Main(string[] args)
        {
            Thread t1 = new Thread(ThreadZaSlanje);
            Thread t2 = new Thread(ThreadZaGasenjeIPaljenje);
            t1.Start();
            t2.Start();
        }
    }
}
