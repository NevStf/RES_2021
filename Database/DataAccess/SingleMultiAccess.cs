using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.DataAccess
{
    public class SingleMultiAccess
    {
        public bool Insert(Dataset_SingleMulti DA)
        {
            try
            {
                using (var db = new DatasetContext())
                {

                    db.Dataset3.Add(DA);
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

        public List<Dataset_SingleMulti> GetAll()
        {
            using (var db = new DatasetContext())
            {

                return db.Dataset3.ToList();

            }
        }

    }
}
