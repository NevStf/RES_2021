using Contracts;
using Contracts.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Writer
{
    public class WriterImplement : IWriter
    {
        public void SendItem()
        {
           

            Random rand = new Random();
            Codes code = (Codes)(rand.Next(1, 8));

            Random rand1 = new Random();
            double value=Math.Round((rand1.NextDouble()*1000),2);

        }

        public void TurnOffWorker()
        {
            throw new NotImplementedException();
        }

        public void TurnOnWorker()
        {
            throw new NotImplementedException();
        }
    }
}
