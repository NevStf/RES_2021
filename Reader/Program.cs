using Contracts;
using Contracts.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Reader
{
    class Program
    {
        static void Main(string[] args)
        {
            int id = 0;
            int code = 0;
            DateTime d1;
            DateTime d2;

            ChannelFactory<IReader> proxy = new ChannelFactory<IReader>(new NetTcpBinding(), new EndpointAddress("net.tcp://localhost:5000/IReader"));

            IReader reader = proxy.CreateChannel();


            // 

            while (true)
            {
                try
                {
                    Console.WriteLine("Unesite ID workera [1 - 4]: ");
                    id = int.Parse(Console.ReadLine());
                    Console.WriteLine("Unesite Code koji zelite da pregledate [1-8]: ");
                    code = int.Parse(Console.ReadLine());
                    Console.WriteLine("Unesite pocetno vreme [primer: 06-Jun-21 5:31:00 PM]: ");
                    d1 = DateTime.Parse(Console.ReadLine());
                    Console.WriteLine("Unesite krajnje vreme [primer: 06-Jun-21 5:31:00 PM]: ");
                    d2 = DateTime.Parse(Console.ReadLine());
                    List<WorkerProperty> list = reader.ReadFromWorker(id, (Codes)code, d1, d2); 
                    
                    foreach (WorkerProperty wp in list)
                    {
                        Console.WriteLine(wp.WorkerID + " " + wp.Code.ToString() + " " + wp.WorkerValue + " " + wp.TimeStamp);
                        
                    }
                    Console.WriteLine("--------------------");
                }
                catch
                {
                    Console.WriteLine("Pogresno uneti podaci, pokusajte ponovo.");
                }

            }

            //Console.WriteLine(DateTime.Now.ToString());
            //Console.WriteLine(d1);
            //Console.WriteLine(d2);
        }
    }
}
