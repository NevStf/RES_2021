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
        private int code;
        private float workerValue;

        [DataMember]
        public int Code { get => code; set => code = value; }

        [DataMember]
        public float WorkerValue { get => workerValue; set => workerValue = value; }

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
