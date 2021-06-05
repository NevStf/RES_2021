using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.DataAccess
{
    public class ConsumerSourceAccess
    {
        public bool Insert(Dataset_ConsumerSource DA)
        {
            try
            {
                using (var db = new DatasetContext())
                {

                    db.Dataset4.Add(DA);
                    db.SaveChanges();
                    return true;
                }
            }
            catch
            {
                Console.WriteLine("Failed");
            }

            return false;
        }

        public List<Dataset_ConsumerSource> GetAll()
        {
            using (var db = new DatasetContext())
            {

                return db.Dataset4.ToList();

            }
        }

        public Dataset_ConsumerSource GetLastConsumer() 
        {
            return GetAll().Where(a => a.Code1 == 7).Last();
        }

        public Dataset_ConsumerSource GetLastSource()
        {
            return GetAll().Where(a => a.Code1 == 8).Last();
        }

    }
}
