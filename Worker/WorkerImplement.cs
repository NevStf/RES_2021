using Contracts;
using Contracts.Resources;
using Database;
using Database.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Worker
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class WorkerImplement : IWorker
    {
        AnalogDigitalAccess dataset1Access = new AnalogDigitalAccess();
        CustomLimitAccess dataset2Access = new CustomLimitAccess();
        SingleMultiAccess dataset3Access = new SingleMultiAccess();
        ConsumerSourceAccess dataset4Access = new ConsumerSourceAccess();

        List<CollectionDescription> LCD1 = new List<CollectionDescription>();
        List<CollectionDescription> LCD2 = new List<CollectionDescription>();
        List<CollectionDescription> LCD3 = new List<CollectionDescription>();
        List<CollectionDescription> LCD4 = new List<CollectionDescription>();
        bool w1 = true, w2 = false, w3 = false, w4 = false;

        public void InitList()
        {
            //CD za prvog workera
            LCD1.Add(new CollectionDescription(1, 1, new List<WorkerProperty>()));
            LCD1.Add(new CollectionDescription(2, 2, new List<WorkerProperty>()));
            LCD1.Add(new CollectionDescription(3, 3, new List<WorkerProperty>()));
            LCD1.Add(new CollectionDescription(4, 4, new List<WorkerProperty>()));

            //za drugog
            LCD2.Add(new CollectionDescription(1, 1, new List<WorkerProperty>()));
            LCD2.Add(new CollectionDescription(2, 2, new List<WorkerProperty>()));
            LCD2.Add(new CollectionDescription(3, 3, new List<WorkerProperty>()));
            LCD2.Add(new CollectionDescription(4, 4, new List<WorkerProperty>()));

            //za treceg
            LCD3.Add(new CollectionDescription(1, 1, new List<WorkerProperty>()));
            LCD3.Add(new CollectionDescription(2, 2, new List<WorkerProperty>()));
            LCD3.Add(new CollectionDescription(3, 3, new List<WorkerProperty>()));
            LCD3.Add(new CollectionDescription(4, 4, new List<WorkerProperty>()));

            //za cetvrtog
            LCD4.Add(new CollectionDescription(1, 1, new List<WorkerProperty>()));
            LCD4.Add(new CollectionDescription(2, 2, new List<WorkerProperty>()));
            LCD4.Add(new CollectionDescription(3, 3, new List<WorkerProperty>()));
            LCD4.Add(new CollectionDescription(4, 4, new List<WorkerProperty>()));

            //SendToBase();
        }

        //provera da li je dataset popunjem prilikom prvog upisa u bazu 
        public void CheckDataset(int IDWorker, CollectionDescription cd)
        {
            if (cd.HistoricalCollection.Count == 2)
            {
                if (cd.DataSet == 1 && dataset1Access.GetAll().Count == 0)
                {
                    SendToBaseFirstTime(IDWorker, cd);
                }
                else if (cd.DataSet == 2 && dataset2Access.GetAll().Count == 0)
                {
                    SendToBaseFirstTime(IDWorker, cd);
                }
                else if (cd.DataSet == 3 && dataset3Access.GetAll().Count == 0)
                {
                    SendToBaseFirstTime(IDWorker, cd);
                }
                else if(cd.DataSet == 4 && dataset4Access.GetAll().Count == 0) 
                {
                    SendToBaseFirstTime(IDWorker, cd);
                }
            }
        }

        public void SendToBaseFirstTime(int IDWorker, CollectionDescription cd)
        {
            if (cd.DataSet == 1)
            {
                Dataset_AnalogDigital DA1 = new Dataset_AnalogDigital { Code1 = (int)cd.HistoricalCollection[0].Code, Value1 = cd.HistoricalCollection[0].WorkerValue, IDWorker = IDWorker };
                Dataset_AnalogDigital DA2 = new Dataset_AnalogDigital { Code1 = (int)cd.HistoricalCollection[1].Code, Value1 = cd.HistoricalCollection[1].WorkerValue, IDWorker = IDWorker };
                dataset1Access.Insert(DA1);
                dataset1Access.Insert(DA2);
            }
            if (cd.DataSet == 2)
            {
                Dataset_CustomLimit DA1 = new Dataset_CustomLimit { Code1 = (int)cd.HistoricalCollection[0].Code, Value1 = cd.HistoricalCollection[0].WorkerValue, IDWorker = IDWorker };
                Dataset_CustomLimit DA2 = new Dataset_CustomLimit { Code1 = (int)cd.HistoricalCollection[1].Code, Value1 = cd.HistoricalCollection[1].WorkerValue, IDWorker = IDWorker };
                dataset2Access.Insert(DA1);
                dataset2Access.Insert(DA2);
            }
            if (cd.DataSet == 3)
            {
                Dataset_SingleMulti DA1 = new Dataset_SingleMulti { Code1 = (int)cd.HistoricalCollection[0].Code, Value1 = cd.HistoricalCollection[0].WorkerValue, IDWorker = IDWorker };
                Dataset_SingleMulti DA2 = new Dataset_SingleMulti { Code1 = (int)cd.HistoricalCollection[1].Code, Value1 = cd.HistoricalCollection[1].WorkerValue, IDWorker = IDWorker };
                dataset3Access.Insert(DA1);
                dataset3Access.Insert(DA2);
            }
            if (cd.DataSet == 4)
            {
                Dataset_ConsumerSource DA1 = new Dataset_ConsumerSource { Code1 = (int)cd.HistoricalCollection[0].Code, Value1 = cd.HistoricalCollection[0].WorkerValue, IDWorker = IDWorker };
                Dataset_ConsumerSource DA2 = new Dataset_ConsumerSource { Code1 = (int)cd.HistoricalCollection[1].Code, Value1 = cd.HistoricalCollection[1].WorkerValue, IDWorker = IDWorker };
                dataset4Access.Insert(DA1);
                dataset4Access.Insert(DA2);
            }
        }

        public bool CheckDeadband(double Val)
        {
            throw new NotImplementedException();
        }

        public void RecieveItem(ListDescription ld)
        {
            if (LCD1.Count == 0 && LCD2.Count == 0 && LCD3.Count == 0 && LCD4.Count == 0)
            {
                InitList();
            }
            Repack(ld);
        }


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

        public void GiveToWorker(ListDescription ld, List<CollectionDescription> cd)
        {
            foreach (Description d in ld.ListOfDescription)
            {
                if (d.Items.Count > 0)
                {
                    if (d.DataSet == 1)
                    {
                        cd[0].AddToHistorical(d.DataSet, d.Items[0].Code, d.Items[0].Value);
                        CheckDataset(ld.WorkerID, cd[0]);
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

                    Console.WriteLine(ld.WorkerID + " Worker prima: " + d.Items[0].Code.ToString() + " i " + d.Items[0].Value);
                }

            }
        }


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
        }

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

        }

        public void SendToBase()
        {
            throw new NotImplementedException();
        }
    }
}
