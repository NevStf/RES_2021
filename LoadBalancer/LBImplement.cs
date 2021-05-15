using Contracts;
using Contracts.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadBalancer
{
    public class LBImplement : ILoadBalancer, IWriter
    {
        
        List<Description> list = new List<Description>();
        
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
            //Console.WriteLine("asdasdasdasdasdasd");
        }

        public void DistributeWork()
        {
            throw new NotImplementedException();
        }

        public void SendItem(Codes code, double value)
        {
            Console.WriteLine("primio code: " + code + "\nPrimio value: " + value); //fali ifologija i sredjivanje u odnosu na dataset
            list[0].Items.Add(new ServerItem(code, value));
            Console.WriteLine(list[0].Items[0].Code);

        }

        public void StoreCodes()
        {
            throw new NotImplementedException();
        }

        public void TurnOffWorker()
        {
            throw new NotImplementedException();
        }

        public void TurnOnWorker()
        {
            throw new NotImplementedException();
        }
    }
}
