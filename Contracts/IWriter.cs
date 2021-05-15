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
    public interface IWriter
    {
        [OperationContract]
        void TurnOnWorker();
        [OperationContract]
        void TurnOffWorker();
        [OperationContract]
        void SendItem();
            
    }

}
