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
    }
}
