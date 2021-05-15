using Contracts.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Worker
{
    [DataContract]
    public class WorkerProperty
    {

        [DataMember]
        public Codes Code { get; set; }

        [DataMember]
        public double WorkerValue { get; set; }

        public WorkerProperty(Codes code, double workerValue)
        {
            Code = code;
            WorkerValue = workerValue;
        }

        public WorkerProperty()
        {
            Code = 0;
            WorkerValue = -1;
        }
    }
}
