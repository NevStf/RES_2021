using Contracts.Resources;
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

        public void AddToHistorical(int dataSet, Codes code, double value) 
        {
            if (this.DataSet == dataSet) {
                WorkerProperty wp = new WorkerProperty(code, value);
                if (this.HistoricalCollection.Count > 0 && this.HistoricalCollection[0].Code == code) 
                {
                    this.HistoricalCollection[0] = wp;
                }
                else
                {
                    this.HistoricalCollection.Add(wp);
                }
            }
        }



    }

    
}
