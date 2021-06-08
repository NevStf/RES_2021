using Contracts;
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
    public class ConnectionWithWorker
    {
        public IWorker Proxy;
        public void Connect()
        {
            ChannelFactory<IWorker> proxy = new ChannelFactory<IWorker>(new NetTcpBinding(),
               new EndpointAddress("net.tcp://localhost:5000/IWorker"));

            Proxy = proxy.CreateChannel();
        }
    }
}
