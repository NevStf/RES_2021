using Contracts;
using Contracts.Resources;
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
        List<CollectionDescription> LCD1 = new List<CollectionDescription>();
        List<CollectionDescription> LCD2 = new List<CollectionDescription>();
        List<CollectionDescription> LCD3 = new List<CollectionDescription>();
        List<CollectionDescription> LCD4 = new List<CollectionDescription>();
        bool w1 = true, w2 = false, w3 = false, w4 = false;

        public void InitList()
        {
            CollectionDescription cd1 = new CollectionDescription(1, 1, new List<WorkerProperty>());
            CollectionDescription cd2 = new CollectionDescription(2, 2, new List<WorkerProperty>());
            CollectionDescription cd3 = new CollectionDescription(3, 3, new List<WorkerProperty>());
            CollectionDescription cd4 = new CollectionDescription(4, 4, new List<WorkerProperty>());

            //CD za prvog workera
            LCD1.Add(cd1);
            LCD1.Add(cd2);
            LCD1.Add(cd3);
            LCD1.Add(cd4);

            //za drugog
            LCD2.Add(cd1);
            LCD2.Add(cd2);
            LCD2.Add(cd3);
            LCD2.Add(cd4);

            //za treceg
            LCD3.Add(cd1);
            LCD3.Add(cd2);
            LCD3.Add(cd3);
            LCD3.Add(cd4);

            //za cetvrtog
            LCD4.Add(cd1);
            LCD4.Add(cd2);
            LCD4.Add(cd3);
            LCD4.Add(cd4);
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

                    }
                    else if (d.DataSet == 2)
                    {
                        cd[1].AddToHistorical(d.DataSet, d.Items[0].Code, d.Items[0].Value);
                    }
                    else if (d.DataSet == 3)
                    {
                        LCD1[2].AddToHistorical(d.DataSet, d.Items[0].Code, d.Items[0].Value);
                    }
                    else
                    {
                        LCD1[3].AddToHistorical(d.DataSet, d.Items[0].Code, d.Items[0].Value);
                    }

                    Console.WriteLine(ld.WorkerID + " Worker prima: " + d.Items[0].Code.ToString() + " i " + d.Items[0].Value);
                }

            }
        }

        public void SendToBase()
        {
            throw new NotImplementedException();
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



    }
}
