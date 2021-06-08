using Contracts;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Worker
{
    [ExcludeFromCodeCoverage]
    class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof(WorkerImplement)))
            {
                string address = "net.tcp://localhost:5000/IWorker";
                string address1 = "net.tcp://localhost:5000/IReader";
                NetTcpBinding binding = new NetTcpBinding();
                host.AddServiceEndpoint(typeof(IWorker), binding, address);
                host.AddServiceEndpoint(typeof(IReader), binding, address1);

                host.Open();
                Console.WriteLine("Worker pokrenut");
                Console.ReadKey();
                host.Close();
            }
        }
    }
}
