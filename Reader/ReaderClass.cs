using Contracts;
using Contracts.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Reader
{
    
    public class ReaderClass
    {
        public int ID { get; set; }
        public int Code { get; set; }
        public DateTime StartDT { get; set; }
        public DateTime EndDT { get; set; }

        public void WriteElements(List<WorkerProperty> l)
        {
            if (l.Count == 0)
            {
                //Console.WriteLine("Nema zapisanih vrednosti za unet vremenski interval.");
                throw new FaultException<CustomException>(new CustomException("Nema zapisanih vrednosti za unet vremenski interval."));
            }
            else
            {
                foreach (WorkerProperty wp in l)
                {
                    Console.WriteLine(wp.WorkerID + " " + wp.Code.ToString() + " " + wp.WorkerValue + " " + wp.TimeStamp);

                }
            }          
        }

        public void Input()
        {
            Console.WriteLine("Unesite ID workera [1 - 4]: ");
            ID = int.Parse(Console.ReadLine());
            if (ID < 1 || ID > 4)
            {
                Console.WriteLine("Pogresan unos, pokusajte ponovo");
                return;
            }

            Console.WriteLine("Unesite Code koji zelite da pregledate [1-8]: ");
            Code = int.Parse(Console.ReadLine());
            if (Code < 1 || Code > 8)
            {

                Console.WriteLine("Pogresan unos, pokusajte ponovo");
                return;
            }
            //Ovde ne mora ifologoija jer ce svakako uci u catch ako bude lose isparsiran datum i vreme
            Console.WriteLine("Unesite pocetno vreme [primer: 06-Jun-21 5:31:00 PM]: ");
            StartDT = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Unesite krajnje vreme [primer: 06-Jun-21 5:31:00 PM]: ");
            EndDT = DateTime.Parse(Console.ReadLine());
        }
    }
}
