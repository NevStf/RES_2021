using Contracts;
using Contracts.Logger;
using Contracts.Resources;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Reader
{

    public class ReaderClass
    {
        Logger logger = new Logger();
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
            logger.WriteToFile(string.Format("{0} Reader primio odgovor od workera sa {1} vrednosti", DateTime.Now.ToString(), l.Count));
        }


        public void Input()
        {
            Console.WriteLine("Unesite ID workera [1 - 4]: ");
            ID = int.Parse(Console.ReadLine());
            if (ID < 1 || ID > 4)
            {
                throw new FaultException<CustomException>(new CustomException("Pogresan unos, pokusajte ponovo."));
            }

            Console.WriteLine("Unesite Code koji zelite da pregledate [1 - 8]: ");
            Code = int.Parse(Console.ReadLine());
            if (Code < 1 || Code > 8)
            {
                throw new FaultException<CustomException>(new CustomException("Pogresan unos, pokusajte ponovo."));
            }

            Console.WriteLine("Unesite pocetno vreme [primer: 06-Jun-21 5:31:00 PM]: ");
            try
            {
                StartDT = DateTime.Parse(Console.ReadLine());
            }
            catch
            {
                throw new FaultException<CustomException>(new CustomException("Pogresan unos, pokusajte ponovo."));
            }
            Console.WriteLine("Unesite krajnje vreme [primer: 06-Jun-21 5:31:00 PM]: ");
            try
            {
                EndDT = DateTime.Parse(Console.ReadLine());
            }
            catch
            {
                throw new FaultException<CustomException>(new CustomException("Pogresan unos, pokusajte ponovo."));
            }
            logger.WriteToFile(string.Format("{0} Reader poslao zahtev workeru {1} za citanje {2} u periodu {3}-{4}",
                DateTime.Now.ToString(), ID, Code, StartDT.ToString(), EndDT.ToString()));
        }

        public ReaderClass(int id, int code, DateTime dt1, DateTime dt2)
        {
            ID = id;
            Code = code;
            StartDT = dt1;
            EndDT = dt2;
        }

        [ExcludeFromCodeCoverage]
        public ReaderClass() { }
    }
}
