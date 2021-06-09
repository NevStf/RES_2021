using Contracts;
using Contracts.Logger;
using Contracts.Resources;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Writer
{
    [ExcludeFromCodeCoverage]
    class Program
    {
        //reference za threadovanje: https://docs.microsoft.com/en-us/dotnet/api/system.threading.thread?view=net-5.0 

        public static void ThreadZaSlanje()
        {
            Logger logger = new Logger();
            ChannelFactory<IWriter> proxy = new ChannelFactory<IWriter>(new NetTcpBinding(),
              new EndpointAddress("net.tcp://localhost:4000/IWriter"));

            IWriter channel = proxy.CreateChannel();
            Random rand = new Random();
            Random rand1 = new Random();
            Console.WriteLine("Writer thread 1 pokrenut");

            channel.InitList(); //inicijalizujem  descriptione i listu descriptiona

            while (true)
            {
                Codes code = (Codes)rand.Next(1, 9);
                //Codes code = (Codes)(rand.Next(1, 3));
                double value = Math.Round((rand1.NextDouble() * 1000), 2);
                channel.WriterToLB(code, value);
                logger.WriteToFile(String.Format("{0} Writer poslao {1} sa {2}", DateTime.Now.ToString(), code.ToString(), value));
                Thread.Sleep(2000);
            }

        }

        public static void ThreadZaGasenjeIPaljenje()
        {
            ChannelFactory<IWriter> proxy = new ChannelFactory<IWriter>(new NetTcpBinding(),
            new EndpointAddress("net.tcp://localhost:4000/IWriter"));

            IWriter channel = proxy.CreateChannel();
            Thread.Sleep(0);
            Logger logger = new Logger();
            logger.WriteToFile(String.Format("{0} Writer uspesno inicijalizovan", DateTime.Now.ToString()));
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
                        logger.WriteToFile(String.Format("{0} Writer poslao zahtev za paljenje workera", DateTime.Now.ToString()));
                    }
                    catch (FaultException<CustomException> e)
                    {
                        Console.WriteLine(e.Detail.CMessage);
                        logger.WriteToFile(String.Format("{0} {1}", DateTime.Now.ToString(), e.Detail.CMessage));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        logger.WriteToFile(String.Format("{0} {1}", DateTime.Now.ToString(), e.Message));
                    }
                }
                else if (res == 2)
                {
                    try
                    {
                        channel.TurnOffWorker();
                        logger.WriteToFile(String.Format("{0} Writer poslao zahtev za gasenje workera", DateTime.Now.ToString()));
                    }
                    catch (FaultException<CustomException> e)
                    {
                        Console.WriteLine(e.Detail.CMessage);
                        logger.WriteToFile(String.Format("{0} {1}", DateTime.Now.ToString(), e.Detail.CMessage));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        logger.WriteToFile(String.Format("{0} {1}", DateTime.Now.ToString(), e.Message));
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
