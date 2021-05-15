using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.Resources;


namespace LoadBalancer
{
    public class ServerItem
    {
        public Codes Code { get; set; }
        public double Value { get; set; }

        public ServerItem(Codes c, double v)
        {
            Code = c;
            Value = v;
        }
    }
}
