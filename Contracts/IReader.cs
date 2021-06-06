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
    public interface IReader
    {
        [OperationContract]
        List<WorkerProperty> ReadFromWorker(int IDWorker, Codes code, DateTime start, DateTime end);
    }
}
