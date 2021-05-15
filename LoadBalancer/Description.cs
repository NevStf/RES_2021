using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadBalancer
{
   public class Description
    {

        public int ID { get; set; }
        public List<ServerItem> Items { get; set; }
        public int DataSet { get; set; }

        public Description()
        {
            ID = 0;
            Items = new List<ServerItem>();
            DataSet = 0;
        }

        public Description(int id, int ds)
        {
            ID = id;
            Items = new List<ServerItem>();
            DataSet = ds;
        }
    }
}
