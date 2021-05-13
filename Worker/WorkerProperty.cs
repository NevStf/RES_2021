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
        public int Code { get; set; }

        [DataMember]
        public float WorkerValue { gets; set; }

        public WorkerProperty(int code, float workerValue)
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
