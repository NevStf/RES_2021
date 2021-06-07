using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    //POZDRAV ZA ADS DUSU MI JE NA NOS IZVADIO :) 
    [DataContract]
    public class CustomException
    {
        [DataMember]
        public string CMessage { get; set; }

        public CustomException(string message)
        {
            CMessage = message;
        }
    }
}
