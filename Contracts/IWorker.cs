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
    public interface IWorker
    {
        [OperationContract]
        void SendToBase(); //Slanje u bazu
        [OperationContract]
        void Repack(Item i); //prpekaivanje za svoju strukturu za rad
        [OperationContract]
        bool CheckDeadband(double Val);
    }
}
