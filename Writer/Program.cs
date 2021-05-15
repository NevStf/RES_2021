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
        static void Main(string[] args)
        {
           ChannelFactory<IWriter> proxy = new ChannelFactory<IWriter>(new NetTcpBinding(),
           new EndpointAddress("net.tcp://localhost:4000/IWriter"));

            IWriter channel = proxy.CreateChannel();
            Random rand = new Random();
            Random rand1 = new Random();
            Console.WriteLine("Writer pokrenut");
            channel.InitList(); //inicijalizujem  descriptione i listu descriptiona
            while (true)
            {
               
                Codes code = (Codes)(rand.Next(1, 8));

                double value = Math.Round((rand1.NextDouble() * 1000), 2);
                channel.SendItem(code, value);

                Console.WriteLine("poslao code: " + code + "\nposlao value: " + value);
                Thread.Sleep(2000);

            }
        
        }
    }
}
