using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadBalancer
{
   public class Description
    {
        int id;
        List<ServerItem> items;
        int ds;

        public int ID { get; set; }
        public List<ServerItem> Items { get; set; }
        public int DataSet { get; set; }

        public Description()
        {
            ID = 0;
           Items  = null;
            DataSet = 0;
        }

        public Description(int id, List<ServerItem> si, int ds)
        {
            ID = id;
            Items = si;
            DataSet = ds;
        }
    }
}
