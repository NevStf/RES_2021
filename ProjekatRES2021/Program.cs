using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database;

namespace ProjekatRES2021
{
    class Program
    {
        static void Main(string[] args)
        {
            using (DatasetContext db = new DatasetContext())
            {
                var DA = new Dataset_AanalogDigital { Code1 = 1, Code2 = 2, Value1 = 76.5, Value2 = 65.5 };
                
                db.Dataset1.Add(DA);
                db.SaveChanges();
            }


            Console.WriteLine("omg hiiiiiiiiiiiii *_*");
            Console.WriteLine("omg hiiiiiiiiiiiii *_*");
            Console.WriteLine("Testiramo 3. put kako se pushuje i pulluje");
            Console.ReadKey();
            
        }
    }
}
