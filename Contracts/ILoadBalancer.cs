using Contracts.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    [ServiceContract]
    public interface ILoadBalancer
    {
        [OperationContract]
        void DistributeWork();
        [OperationContract]
        void SendToWorker(ListDescription ld);
        

    }
}
