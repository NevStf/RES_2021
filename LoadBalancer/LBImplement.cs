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

namespace LoadBalancer
{

    public class LBImplement : ILoadBalancer, IWriter
    {
        Logger logger = new Logger();
        public List<Description> list = new List<Description>();
        //public List<Description> List { get; set; }

        public static int brojac = 1; //brojac workera, u pocetku uvek imamo jednog!
        public static int Brojac { get { return brojac; } set { brojac = value; } }
        public static int DistributeCount = 0;
        static bool w1 = true, w2 = false, w3 = false, w4 = false; //mora static jer ne radi ako nije static
        ConnectionWithWorker cww = new ConnectionWithWorker();

        public void InitList() //inicijalizacija liste descriptiona
        {
            Description d1 = new Description(1, 1);
            Description d2 = new Description(2, 2);
            Description d3 = new Description(3, 3);
            Description d4 = new Description(4, 4);
            list.Add(d1);
            list.Add(d2);
            list.Add(d3);
            list.Add(d4);
        }

        [ExcludeFromCodeCoverage]
        //Podeli posao round robin sistemom
        public void DistributeWork(ListDescription ld)
        {
            if (DistributeCount == 0)
            {
                if (Brojac > 1)
                {
                    DistributeCount++;
                }
                ld.WorkerID = 1;

            }
            else if (DistributeCount == 1)
            {

                if (Brojac >= 2 && w3 == true)
                {
                    DistributeCount++;
                }
                else
                {
                    DistributeCount = 0;
                }
                ld.WorkerID = 2;

            }
            else if (DistributeCount == 2)
            {
                if (Brojac >= 3 && w4 == true)
                {
                    DistributeCount++;
                }
                else
                {
                    DistributeCount = 0;
                }
                ld.WorkerID = 3;

            }
            else
            {
                ld.WorkerID = 4;
                DistributeCount = 0;
            }
            SendToWorker(ld);
        }

        public ListDescription InitListDesc()
        {
            ListDescription ld = new ListDescription();
            ld.ListOfDescription = new List<Description>();
            Description d1 = new Description(1, 1);
            Description d2 = new Description(2, 2);
            Description d3 = new Description(3, 3);
            Description d4 = new Description(4, 4);
            ld.ListOfDescription.Add(d1);
            ld.ListOfDescription.Add(d2);
            ld.ListOfDescription.Add(d3);
            ld.ListOfDescription.Add(d4);

            return ld;

        }

        [ExcludeFromCodeCoverage]
        public void WriterToLB(Codes code, double value)
        {
            ListDescription ld = InitListDesc();
            Item si = new Item(code, value);

            Console.WriteLine("Primio code: " + code + "\nPrimio value: " + value);

            if (code == Codes.CODE_ANALOG || code == Codes.CODE_DIGITAL)
            {
                list[0].Items.Add(si);
                ld.ListOfDescription[0].Items.Add(si);
            }
            else if (code == Codes.CODE_CUSTOM || code == Codes.CODE_LIMITSET)
            {
                list[1].Items.Add(si);
                ld.ListOfDescription[1].Items.Add(si);
            }
            else if (code == Codes.CODE_SINGLEONE || code == Codes.CODE_MULTIPLEONE)
            {
                list[2].Items.Add(si);
                ld.ListOfDescription[2].Items.Add(si);
            }
            else
            {
                list[3].Items.Add(si);
                ld.ListOfDescription[3].Items.Add(si);
            }
            logger.WriteToFile(String.Format("{0} LB primio {1} sa {2}", DateTime.Now.ToString(), code.ToString(), value));
            DistributeWork(ld);
        }

        [ExcludeFromCodeCoverage]
        public void SendToWorker(ListDescription ld)
        {
            logger.WriteToFile(String.Format("{0} LB prosledio parametre workeru {1}", DateTime.Now.ToString(), ld.WorkerID));
            cww.Connect();
            cww.Proxy.RecieveItem(ld);
        }

        public void TurnOffWorker()
        {
            logger.WriteToFile(String.Format("{0} LB primio zahtev za gasenje workera", DateTime.Now.ToString()));
            if (Brojac == 1)
            {
                logger.WriteToFile(String.Format("{0} LB odbio zahtev za gasenje workera (samo jedan aktivan)", DateTime.Now.ToString()));
                throw new FaultException<CustomException>(new CustomException("Samo je jedan worker ukljucen."));
            }
            else
            {
                Brojac--;
                cww.Connect();
                cww.Proxy.ITurnOff(Brojac);
                if (Brojac == 3)
                {
                    w4 = false;
                }
                else if (Brojac == 2)
                {
                    w3 = false;
                }
                else
                {
                    w2 = false;
                }
                Console.WriteLine("Primio sam poruku i ugasio " + Brojac + ". workera");
                logger.WriteToFile(String.Format("{0} LB iskljucio workera", DateTime.Now.ToString()));
            }
        }

        public void TurnOnWorker()
        {
            logger.WriteToFile(String.Format("{0} LB primio zahtev za ukljucenje workera", DateTime.Now.ToString()));
            if (Brojac == 4)
            {
                logger.WriteToFile(String.Format("{0} LB odbio zahtev za ukljucenje workera (svi su aktivni)", DateTime.Now.ToString()));
                throw new FaultException<CustomException>(new CustomException("Sva cetiri workera su ukljucena."));
            }
            else
            {
                Brojac++;
                cww.Connect();
                cww.Proxy.ITurnOn(Brojac);
                if (Brojac == 2)
                {
                    w2 = true;
                }
                else if (Brojac == 3)
                {
                    w3 = true;
                }
                else
                {
                    w4 = true;
                }

                Console.WriteLine("Primio sam poruku i ukljucio " + Brojac + ". workera.");
                logger.WriteToFile(String.Format("{0} LB ukljucio workera", DateTime.Now.ToString()));
            }
        }
    }
}