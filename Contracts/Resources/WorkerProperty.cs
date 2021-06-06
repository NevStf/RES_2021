using Contracts.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Resources
{
    [DataContract]
    public class WorkerProperty
    {

        [DataMember]
        public Codes Code { get; set; }

        [DataMember]
        public double WorkerValue { get; set; }
        [DataMember]
        public DateTime TimeStamp { get; set; }
        [DataMember]
        public int WorkerID { get; set; }

        public WorkerProperty(Codes code, double workerValue)
        {
            Code = code;
            WorkerValue = workerValue;
        }

        public WorkerProperty(int ID, Codes c, double v, DateTime dt)
        {
            WorkerID = ID;
            Code = c;
            WorkerValue = v;
            TimeStamp = dt;
        }
    }
}
