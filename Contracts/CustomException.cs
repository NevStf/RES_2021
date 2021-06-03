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

        string message;
     
        [DataMember]
        public string Message { get => message; set => message = value; }

        public CustomException(string message)
        {
            Message = message;
        }

        public CustomException() : this("") { }

    }
}
