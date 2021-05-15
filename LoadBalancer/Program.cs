using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace LoadBalancer
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof(LBImplement)))
            {
                string address = "net.tcp://localhost:4000/IWriter";
                NetTcpBinding binding = new NetTcpBinding();
                host.AddServiceEndpoint(typeof(IWriter), binding, address);

                host.Open();
                
                Console.ReadKey();
                host.Close();
            }
        }
    }
}
