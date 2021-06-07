using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Resources
{

    public enum Codes
    {
        CODE_ANALOG = 1, //stavljamo 1 da bi analog imao flag 1, digital 2 ... 
        CODE_DIGITAL,
        CODE_CUSTOM,
        CODE_LIMITSET,
        CODE_SINGLEONE,
        CODE_MULTIPLEONE,
        CODE_CONSUMER,
        CODE_SOURCE

    }

}
