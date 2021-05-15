using Contracts;
using Contracts.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Worker
{
    public class WorkerImplement : IWorker
    {
        public bool CheckDeadband(double Val)
        {
            throw new NotImplementedException();
        }

        public void Repack(Item i)
        {
            WorkerProperty wp = new WorkerProperty(i.Code, i.Value);
            Console.WriteLine(wp.Code + " "+ wp.WorkerValue);
        }

        public void SendToBase()
        {
            throw new NotImplementedException();
        }
    }
}
