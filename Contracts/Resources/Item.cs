using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Resources
{
    [DataContract]
    public class Item
    {
        [DataMember]
        public Codes Code { get; set; }
        [DataMember]
        public double Value { get; set; }

        public Item(Codes c, double v)
        {
            Code = c;
            Value = v;
        }
    }
}
