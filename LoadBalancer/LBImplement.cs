﻿using Contracts;
using Contracts.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace LoadBalancer
{

    public class LBImplement : ILoadBalancer, IWriter
    {
        List<Description> list = new List<Description>();
        public static int Brojac = 1; //brojac workera, u pocetku uvek imamo jednog!
        public static int DistributeCount = 0;
        static bool w1 = true, w2 = false, w3 = false, w4 = false; //mora static jer ne radi ako nije static

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
                SendToWorker(ld);
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
                SendToWorker(ld);
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
                SendToWorker(ld);
            }
            else
            {
                ld.WorkerID = 4;
                SendToWorker(ld);
                DistributeCount = 0;
            }
        }

        public void WriterToLB(Codes code, double value)
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
            else if (code == Codes.CODE_SINGLEONE || code == Codes.CODE_MULTIPLENODE)
            {
                list[2].Items.Add(si);
                ld.ListOfDescription[2].Items.Add(si);
            }
            else
            {
                list[3].Items.Add(si);
                ld.ListOfDescription[3].Items.Add(si);
            }
            DistributeWork(ld);
        }
        public void SendToWorker(ListDescription ld)
        {
            ChannelFactory<IWorker> proxy = new ChannelFactory<IWorker>(new NetTcpBinding(),
           new EndpointAddress("net.tcp://localhost:5000/IWorker"));

            IWorker worker = proxy.CreateChannel();
            worker.RecieveItem(ld);
        }

        public void TurnOffWorker()
        {
            if (Brojac == 1)
            {
                throw new FaultException<CustomException>(new CustomException("Samo je jedan worker ukljucen."));
            }
            else
            {
                Brojac--;
                ChannelFactory<IWorker> proxy = new ChannelFactory<IWorker>(new NetTcpBinding(),
                new EndpointAddress("net.tcp://localhost:5000/IWorker"));

                IWorker worker = proxy.CreateChannel();
                worker.ITurnOff(Brojac);
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
            }
        }

        public void TurnOnWorker()
        {
            if (Brojac == 4)
            {
                throw new FaultException<CustomException>(new CustomException("Sva cetiri workera su ukljucena."));
            }
            else
            {
                Brojac++;
                ChannelFactory<IWorker> proxy = new ChannelFactory<IWorker>(new NetTcpBinding(),
                new EndpointAddress("net.tcp://localhost:5000/IWorker"));

                IWorker worker = proxy.CreateChannel();
                worker.ITurnOn(Brojac);
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
            }
        }
    }
}