using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Worker
{
    [DataContract]
    public class CollectionDescription
    {

        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public int DataSet { get; set; }

        [DataMember]
        public List<WorkerProperty> HistoricalCollection { get; set; }

        

        public CollectionDescription(int id, int dataSet, List<WorkerProperty> historicalCollection)
        {
            ID = id;
            DataSet = dataSet;
            HistoricalCollection = historicalCollection;
            
        }

        public CollectionDescription()
        {
            ID = -5;
            DataSet = -5;
            HistoricalCollection = new List<WorkerProperty>();
            
        }
    }

    
}
