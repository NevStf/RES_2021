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
        [FaultContract(typeof(CustomException))]
        void TurnOnWorker();
        [OperationContract]
        [FaultContract(typeof(CustomException))]
        void TurnOffWorker();
        [OperationContract]
        void WriterToLB(Codes code, double values);
        [OperationContract]
        void InitList();
        
    }

}
