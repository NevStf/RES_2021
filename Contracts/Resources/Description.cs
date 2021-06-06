using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Resources
{
    public class Description
    {
        public int ID { get; set; }
        public List<Item> Items { get; set; }
        public int DataSet { get; set; }

        public Description(int id, int ds)
        {
            ID = id;
            Items = new List<Item>();
            DataSet = ds;
        }
    }
}
