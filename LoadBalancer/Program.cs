using Contracts;
using Contracts.Logger;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace LoadBalancer
{
    [ExcludeFromCodeCoverage]
    class Program
    {
        static void Main(string[] args)
        {
            Logger logger = new Logger();
            using (ServiceHost host = new ServiceHost(typeof(LBImplement)))
            {
                string address = "net.tcp://localhost:4000/IWriter";
                NetTcpBinding binding = new NetTcpBinding();
                host.AddServiceEndpoint(typeof(IWriter), binding, address);

                host.Open();
                logger.WriteToFile(String.Format("{0} LB uspesno inicijalizovan i ceka poruke", DateTime.Now.ToString()));
                Console.ReadKey();
                host.Close();
            }
        }
    }
}
