using Contracts.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IWriter
    {
        void TurnOnWorker();
        void TurnOffWorker();
        void CreateItem(Codes code, double val);
            
    }

}
