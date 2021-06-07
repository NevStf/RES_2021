using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Resources
{
    [ExcludeFromCodeCoverage]
    [DataContract]
    public class ListDescription
    {
        [DataMember]
        public List<Description> ListOfDescription { get; set; }
        [DataMember]
        public int WorkerID { get; set; } //Da znamo kom workeru saljemo koji LD 
    }
}
