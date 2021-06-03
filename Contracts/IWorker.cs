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
        void Repack(ListDescription ld); //prpekaivanje za svoju strukturu za rad
        [OperationContract]
        bool CheckDeadband(double Val);
        [OperationContract]
        void RecieveItem(ListDescription ld);
        [OperationContract]
        void ITurnOff(int count); //worker se gasi
        [OperationContract]
        void ITurnOn(int count); //worker se pali 
    }
}
