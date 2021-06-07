using Contracts;
using Contracts.Resources;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Reader
{
    [ExcludeFromCodeCoverage]
    class Program
    {
        static void Main(string[] args)
        {
            ReaderClass r = new ReaderClass();
            ChannelFactory<IReader> proxy = new ChannelFactory<IReader>(new NetTcpBinding(), new EndpointAddress("net.tcp://localhost:5000/IReader"));

            IReader reader = proxy.CreateChannel();

            while (true)
            {
                try
                {
                    r.Input();
                    List<WorkerProperty> list = reader.ReadFromWorker(r.ID, (Codes)r.Code, r.StartDT, r.EndDT);
                    r.WriteElements(list);
                    Console.WriteLine("-------------------------");
                }
                catch(FaultException<CustomException> e)
                {
                    Console.WriteLine(e.Detail.CMessage);
                }
            }
        }
    }
}
