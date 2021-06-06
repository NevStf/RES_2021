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
        [OperationContract] // <-- ne mora da sadrzi operation contract
        void Repack(ListDescription ld); //prpekaivanje za svoju strukturu za rad

        [OperationContract] // <-- ne mora da sadrzi operation contract
        bool CheckDeadband(int dataset, object o);
        [OperationContract]
        void RecieveItem(ListDescription ld);
        [OperationContract]
        void ITurnOff(int count); //worker se gasi
        [OperationContract]
        void ITurnOn(int count); //worker se pali 
        
    }
}
