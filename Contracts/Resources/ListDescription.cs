﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Resources
{
    [DataContract]
    public class ListDescription
    {
        [DataMember]
        public List<Description> ListOfDescription { get; set; }
        
    }
}
