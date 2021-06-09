using Contracts;
using Contracts.Logger;
using Contracts.Resources;
using Database;
using Database.DataAccess;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Worker
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)] //singleton, reference: https://www.codeproject.com/Articles/86007/3-ways-to-do-WCF-instance-management-Per-call-Per
    public class WorkerImplement : IWorker, IReader
    {
        Logger logger = new Logger();
        AnalogDigitalAccess dataset1Access = new AnalogDigitalAccess();
        CustomLimitAccess dataset2Access = new CustomLimitAccess();
        SingleMultiAccess dataset3Access = new SingleMultiAccess();
        ConsumerSourceAccess dataset4Access = new ConsumerSourceAccess();

        //Za svakog workera, njegova lista collectiona
        public List<CollectionDescription> LCD1 = new List<CollectionDescription>();
        public List<CollectionDescription> LCD2 = new List<CollectionDescription>();
        public List<CollectionDescription> LCD3 = new List<CollectionDescription>();
        public List<CollectionDescription> LCD4 = new List<CollectionDescription>();
        public CollectionDescription History = new CollectionDescription(0, 0);

        bool w1 = true, w2 = false, w3 = false, w4 = false;
        bool firsttime = true; //da ne upisuje duplo digital code nakon prvog upisa u situaciji ANALOG, DIGITAL

        public void InitList() //inicijalizuj mi listu samo jendom svaki put kada se ukljuci program
        {
            //CD za prvog workera
            LCD1.Add(new CollectionDescription(1, 1));
            LCD1.Add(new CollectionDescription(2, 2));
            LCD1.Add(new CollectionDescription(3, 3));
            LCD1.Add(new CollectionDescription(4, 4));

            //za drugog
            LCD2.Add(new CollectionDescription(1, 1));
            LCD2.Add(new CollectionDescription(2, 2));
            LCD2.Add(new CollectionDescription(3, 3));
            LCD2.Add(new CollectionDescription(4, 4));

            //za treceg
            LCD3.Add(new CollectionDescription(1, 1));
            LCD3.Add(new CollectionDescription(2, 2));
            LCD3.Add(new CollectionDescription(3, 3));
            LCD3.Add(new CollectionDescription(4, 4));

            //za cetvrtog
            LCD4.Add(new CollectionDescription(1, 1));
            LCD4.Add(new CollectionDescription(2, 2));
            LCD4.Add(new CollectionDescription(3, 3));
            LCD4.Add(new CollectionDescription(4, 4));
        }

        [ExcludeFromCodeCoverage]
        //provera da li je dataset popunjem prilikom prvog upisa u bazu 
        public void CheckDataset(int IDWorker, CollectionDescription cd)
        {
            if (cd.HistoricalCollection.Count == 2)
            {
                if (cd.DataSet == 1 && dataset1Access.GetAll().Count == 0)
                {
                    SendToBaseFirstTime(IDWorker, cd);
                    firsttime = false;
                }
                else if (cd.DataSet == 2 && dataset2Access.GetAll().Count == 0)
                {
                    SendToBaseFirstTime(IDWorker, cd);
                }
                else if (cd.DataSet == 3 && dataset3Access.GetAll().Count == 0)
                {
                    SendToBaseFirstTime(IDWorker, cd);
                }
                else if (cd.DataSet == 4 && dataset4Access.GetAll().Count == 0)
                {
                    SendToBaseFirstTime(IDWorker, cd);
                }
            }
        }

        [ExcludeFromCodeCoverage]
        public void SendToBaseFirstTime(int IDWorker, CollectionDescription cd)
        {
            logger.WriteToFile(string.Format("{0} Upis u bazu {1} sa {2}", DateTime.Now.ToString(), cd.HistoricalCollection[0].Code.ToString(), cd.HistoricalCollection[0].WorkerValue));
            if (cd.DataSet == 1)
            {
                Dataset_AnalogDigital DA1 = new Dataset_AnalogDigital { Code1 = (int)cd.HistoricalCollection[0].Code, Value1 = cd.HistoricalCollection[0].WorkerValue, IDWorker = IDWorker };
                Dataset_AnalogDigital DA2 = new Dataset_AnalogDigital { Code1 = (int)cd.HistoricalCollection[1].Code, Value1 = cd.HistoricalCollection[1].WorkerValue, IDWorker = IDWorker };
                //Insertuj u tabelu kojoj odgovaraju kodovi
                dataset1Access.Insert(DA1);
                dataset1Access.Insert(DA2);
                Console.WriteLine("Worker sa IDjem " + IDWorker + " je upisao " + DA1.Code1.ToString() + " i " + DA1.Value1);
                Console.WriteLine("Worker sa IDjem " + IDWorker + " je upisao " + DA2.Code1.ToString() + " i " + DA2.Value1);
            }
            else if (cd.DataSet == 2)
            {
                Dataset_CustomLimit DA1 = new Dataset_CustomLimit { Code1 = (int)cd.HistoricalCollection[0].Code, Value1 = cd.HistoricalCollection[0].WorkerValue, IDWorker = IDWorker };
                Dataset_CustomLimit DA2 = new Dataset_CustomLimit { Code1 = (int)cd.HistoricalCollection[1].Code, Value1 = cd.HistoricalCollection[1].WorkerValue, IDWorker = IDWorker };
                //Insertuj u tabelu kojoj odgovaraju kodovi
                dataset2Access.Insert(DA1);
                dataset2Access.Insert(DA2);
                Console.WriteLine("Worker sa IDjem " + IDWorker + " je upisao " + DA1.Code1.ToString() + " i " + DA1.Value1);
                Console.WriteLine("Worker sa IDjem " + IDWorker + " je upisao " + DA2.Code1.ToString() + " i " + DA2.Value1);
            }
            else if (cd.DataSet == 3)
            {
                Dataset_SingleMulti DA1 = new Dataset_SingleMulti { Code1 = (int)cd.HistoricalCollection[0].Code, Value1 = cd.HistoricalCollection[0].WorkerValue, IDWorker = IDWorker };
                Dataset_SingleMulti DA2 = new Dataset_SingleMulti { Code1 = (int)cd.HistoricalCollection[1].Code, Value1 = cd.HistoricalCollection[1].WorkerValue, IDWorker = IDWorker };
                //Insertuj u tabelu kojoj odgovaraju kodovi
                dataset3Access.Insert(DA1);
                dataset3Access.Insert(DA2);
                Console.WriteLine("Worker sa IDjem " + IDWorker + " je upisao " + DA1.Code1.ToString() + " i " + DA1.Value1);
                Console.WriteLine("Worker sa IDjem " + IDWorker + " je upisao " + DA2.Code1.ToString() + " i " + DA2.Value1);
            }
            else if (cd.DataSet == 4)
            {
                Dataset_ConsumerSource DA1 = new Dataset_ConsumerSource { Code1 = (int)cd.HistoricalCollection[0].Code, Value1 = cd.HistoricalCollection[0].WorkerValue, IDWorker = IDWorker };
                Dataset_ConsumerSource DA2 = new Dataset_ConsumerSource { Code1 = (int)cd.HistoricalCollection[1].Code, Value1 = cd.HistoricalCollection[1].WorkerValue, IDWorker = IDWorker };
                //Insertuj u tabelu kojoj odgovaraju kodovi
                dataset4Access.Insert(DA1);
                dataset4Access.Insert(DA2);
                Console.WriteLine("Worker sa IDjem " + IDWorker + " je upisao " + DA1.Code1.ToString() + " i " + DA1.Value1);
                Console.WriteLine("Worker sa IDjem " + IDWorker + " je upisao " + DA2.Code1.ToString() + " i " + DA2.Value1);
            }
        }

        [ExcludeFromCodeCoverage]
        //Proveri da li prolazi deadband
        public bool CheckDeadband(int dataset, object workerProperty)
        {
            WorkerProperty wp = workerProperty as WorkerProperty;

            if (wp.Code == Codes.CODE_DIGITAL)
            {
                Console.WriteLine("Prosao deadband za " + wp.Code.ToString());
                logger.WriteToFile(string.Format("{0} Prosao deadband za {1} sa {2}", DateTime.Now.ToString(), wp.Code.ToString(), wp.WorkerValue));
                return true;
            }
            if (wp.Code == Codes.CODE_ANALOG)
            {
                Dataset_AnalogDigital da = dataset1Access.GetLastAnalog();
                if (wp.WorkerValue <= da.Value1 * 0.98 || wp.WorkerValue >= da.Value1 * 1.02)
                {
                    Console.WriteLine("Prosao deadband za " + wp.Code.ToString());
                    logger.WriteToFile(string.Format("{0} Prosao deadband za {1} sa {2}", DateTime.Now.ToString(), wp.Code.ToString(), wp.WorkerValue));
                    return true;
                }
            }
            if (wp.Code == Codes.CODE_CUSTOM)
            {
                Dataset_CustomLimit da = dataset2Access.GetLastCustom();
                if (wp.WorkerValue <= da.Value1 * 0.98 || wp.WorkerValue >= da.Value1 * 1.02)
                {
                    Console.WriteLine("Prosao deadband za " + wp.Code.ToString());
                    logger.WriteToFile(string.Format("{0} Prosao deadband za {1} sa {2}", DateTime.Now.ToString(), wp.Code.ToString(), wp.WorkerValue));
                    return true;
                }
            }
            if (wp.Code == Codes.CODE_LIMITSET)
            {
                Dataset_CustomLimit da = dataset2Access.GetLastLimit();
                if (wp.WorkerValue <= da.Value1 * 0.98 || wp.WorkerValue >= da.Value1 * 1.02)
                {
                    Console.WriteLine("Prosao deadband za " + wp.Code.ToString());
                    logger.WriteToFile(string.Format("{0} Prosao deadband za {1} sa {2}", DateTime.Now.ToString(), wp.Code.ToString(), wp.WorkerValue));
                    return true;
                }
            }
            if (wp.Code == Codes.CODE_MULTIPLEONE)
            {
                Dataset_SingleMulti da = dataset3Access.GetLastMulti();
                if (wp.WorkerValue <= da.Value1 * 0.98 || wp.WorkerValue >= da.Value1 * 1.02)
                {
                    Console.WriteLine("Prosao deadband za " + wp.Code.ToString());
                    logger.WriteToFile(string.Format("{0} Prosao deadband za {1} sa {2}", DateTime.Now.ToString(), wp.Code.ToString(), wp.WorkerValue));
                    return true;
                }
            }
            if (wp.Code == Codes.CODE_SINGLEONE)
            {
                Dataset_SingleMulti da = dataset3Access.GetLastSingle();
                if (wp.WorkerValue <= da.Value1 * 0.98 || wp.WorkerValue >= da.Value1 * 1.02)
                {
                    Console.WriteLine("Prosao deadband za " + wp.Code.ToString());
                    logger.WriteToFile(string.Format("{0} Prosao deadband za {1} sa {2}", DateTime.Now.ToString(), wp.Code.ToString(), wp.WorkerValue));
                    return true;
                }
            }
            if (wp.Code == Codes.CODE_CONSUMER)
            {
                Dataset_ConsumerSource da = dataset4Access.GetLastConsumer();
                if (wp.WorkerValue <= da.Value1 * 0.98 || wp.WorkerValue >= da.Value1 * 1.02)
                {
                    Console.WriteLine("Prosao deadband za " + wp.Code.ToString());
                    logger.WriteToFile(string.Format("{0} Prosao deadband za {1} sa {2}", DateTime.Now.ToString(), wp.Code.ToString(), wp.WorkerValue));
                    return true;
                }
            }
            if (wp.Code == Codes.CODE_SOURCE)
            {
                Dataset_ConsumerSource da = dataset4Access.GetLastSource();
                if (wp.WorkerValue <= da.Value1 * 0.98 || wp.WorkerValue >= da.Value1 * 1.02)
                {
                    Console.WriteLine("Prosao deadband za " + wp.Code.ToString());
                    logger.WriteToFile(string.Format("{0} Prosao deadband za {1} sa {2}", DateTime.Now.ToString(), wp.Code.ToString(), wp.WorkerValue));
                    return true;
                }
            }
            Console.WriteLine("NIJE Prosao deadband za " + wp.Code.ToString());
            logger.WriteToFile(string.Format("{0} NIJE Prosao deadband za {1} sa {2}", DateTime.Now.ToString(), wp.Code.ToString(), wp.WorkerValue));
            return false;
        }

        [ExcludeFromCodeCoverage]
        //ukoliko u bazi postoji par, vrati true
        public bool CheckDatabasePopulated(int dataset)
        {
            if (dataset == 1 && dataset1Access.GetAll().Count >= 2)
            {
                return true;
            }
            else if (dataset == 2 && dataset2Access.GetAll().Count >= 2)
            {
                return true;
            }
            else if (dataset == 3 && dataset3Access.GetAll().Count >= 2)
            {
                return true;
            }
            else if (dataset == 4 && dataset4Access.GetAll().Count >= 2)
            {
                return true;
            }

            return false;
        }

        [ExcludeFromCodeCoverage]
        //WP ce biti poslat u bazu samo ukoliko u bazi postoji par vrednosti (D-A, C-L, S-M, C-S)
        public void SendToBase(int IDWorker, int dataset, WorkerProperty wp)
        {
            if (CheckDatabasePopulated(dataset) && CheckDeadband(dataset, wp))
            {
                if (dataset == 1)
                {
                    var DA = new Dataset_AnalogDigital { Code1 = (int)wp.Code, Value1 = wp.WorkerValue, IDWorker = IDWorker };

                    dataset1Access.Insert(DA);
                }
                else if (dataset == 2)
                {
                    var CL = new Dataset_CustomLimit { Code1 = (int)wp.Code, Value1 = wp.WorkerValue, IDWorker = IDWorker };
                    dataset2Access.Insert(CL);
                }
                else if (dataset == 3)
                {
                    var SM = new Dataset_SingleMulti { Code1 = (int)wp.Code, Value1 = wp.WorkerValue, IDWorker = IDWorker };
                    dataset3Access.Insert(SM);
                }
                else
                {
                    var CS = new Dataset_ConsumerSource { Code1 = (int)wp.Code, Value1 = wp.WorkerValue, IDWorker = IDWorker };
                    dataset4Access.Insert(CS);
                }
                logger.WriteToFile(string.Format("{0} Upis u bazu {1} sa {2}", DateTime.Now.ToString(), wp.Code.ToString(), wp.WorkerValue));
            }
        }

        [ExcludeFromCodeCoverage]
        //Inicijalizuj liste i prepakuj u Worker strukturu za rad
        public void RecieveItem(ListDescription ld)
        {
            if (LCD1.Count == 0 && LCD2.Count == 0 && LCD3.Count == 0 && LCD4.Count == 0)
            {
                InitList();
            }
            //ako vec postoje elementi u bazi, stavi firsttime na false
            if (dataset1Access.GetAll().Count != 0)
            {
                firsttime = false;
            }
            Repack(ld);
        }

        [ExcludeFromCodeCoverage]
        //Prepakuj i daj workeru na citanje
        public void Repack(ListDescription ld)
        {
            if (ld.WorkerID == 1)
            {
                GiveToWorker(ld, LCD1);
            }
            else if (ld.WorkerID == 2)
            {
                GiveToWorker(ld, LCD2);
            }
            else if (ld.WorkerID == 3)
            {
                GiveToWorker(ld, LCD3);
            }
            else
            {
                GiveToWorker(ld, LCD4);
            }
        }

        [ExcludeFromCodeCoverage]
        public void GiveToWorker(ListDescription ld, List<CollectionDescription> cd)
        {
            foreach (Description d in ld.ListOfDescription)
            {
                if (d.Items.Count > 0)
                {
                    Console.WriteLine(ld.WorkerID + " Worker prima: " + d.Items[0].Code.ToString() + " i " + d.Items[0].Value); //for testing purposes only 
                    logger.WriteToFile(string.Format("{0} Worker primio {1} sa {2}", DateTime.Now.ToString(), d.Items[0].Code, d.Items[0].Value));
                    ValueHistory(new WorkerProperty(ld.WorkerID, d.Items[0].Code, d.Items[0].Value, DateTime.Now)); //Ubaci u listu za readera
                    if (d.DataSet == 1)
                    {
                        cd[0].AddToHistorical(d.DataSet, d.Items[0].Code, d.Items[0].Value);
                        if (firsttime) //uslov je zbog digital koda koji uvek prolazi deadband (ovaj uslov moze za svaki dataset zbog brzine i optimizacije da se ubaci) 
                        {
                            CheckDataset(ld.WorkerID, cd[0]);
                            return;
                        }
                    }
                    else if (d.DataSet == 2)
                    {
                        cd[1].AddToHistorical(d.DataSet, d.Items[0].Code, d.Items[0].Value);
                        CheckDataset(ld.WorkerID, cd[1]);
                    }
                    else if (d.DataSet == 3)
                    {
                        cd[2].AddToHistorical(d.DataSet, d.Items[0].Code, d.Items[0].Value);
                        CheckDataset(ld.WorkerID, cd[2]);
                    }
                    else
                    {
                        cd[3].AddToHistorical(d.DataSet, d.Items[0].Code, d.Items[0].Value);
                        CheckDataset(ld.WorkerID, cd[3]);
                    }
                    SendToBase(ld.WorkerID, d.DataSet, new WorkerProperty(d.Items[0].Code, d.Items[0].Value));
                }
            }
        }

        //Istorija svih vrednosti, bez obzira da li je upisano u bazu ili ne
        public void ValueHistory(WorkerProperty wp)
        {
            History.HistoricalCollection.Add(wp);
        }

        //Reader cita iz workera
        public List<WorkerProperty> ReadFromWorker(int IDWorker, Codes code, DateTime start, DateTime end)
        {
            logger.WriteToFile(string.Format("{0} Worker{1} primio zahtev od readera za {2} u periodu {3}-{4}", DateTime.Now.ToString(), IDWorker, code.ToString(), start.ToString(), end.ToString()));
            List<WorkerProperty> list = History.HistoricalCollection.Where(id => id.WorkerID == IDWorker && id.Code == code).ToList(); //filtriranje liste pomocu lamda izraza
            List<WorkerProperty> retVal = new List<WorkerProperty>(); //nova lista koju ce da primi reader
            foreach (WorkerProperty wp in list)
            {
                if (start < wp.TimeStamp && end > wp.TimeStamp)
                {
                    retVal.Add(wp);
                }
            }

            return retVal;
        }

        //Provera za iskljucene workere
        public void ITurnOff(int count)
        {
            if (count == 3)
            {
                w4 = false;
            }
            else if (count == 2)
            {
                w3 = false;
            }
            else if (count == 1)
            {
                w2 = false;
            }
            logger.WriteToFile(string.Format("{0} Ukljucen worker {1}", DateTime.Now.ToString(), count + 1));
        }

        //Provera za ukljucene workere
        public void ITurnOn(int count)
        {
            if (count == 2)
            {
                w2 = true;
            }
            else if (count == 3)
            {
                w3 = true;
            }
            else if (count == 4)
            {
                w4 = true;
            }
            logger.WriteToFile(string.Format("{0} Ukljucen worker {1}", DateTime.Now.ToString(), count));
        }
    }
}
