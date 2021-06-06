using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Resources
{
    [DataContract]
    public class Description
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public List<Item> Items { get; set; }
        [DataMember]
        public int DataSet { get; set; }

        public Description(int id, int ds)
        {
            ID = id;
            Items = new List<Item>();
            DataSet = ds;
        }

        //bez ovoga ne radi, jer .. ne znam.

        public Description()
        {
            ID = -5;
            DataSet = -5;
            Items = new List<Item>();
        }
    }
}
