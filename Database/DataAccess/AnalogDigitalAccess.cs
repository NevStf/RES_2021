using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.DataAccess
{
    public class AnalogDigitalAccess
    {
        public bool Insert(Dataset_AnalogDigital DA)
        {
            try
            {
                using (var db = new DatasetContext())
                {

                    db.Dataset1.Add(DA);
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

        public List<Dataset_AnalogDigital> GetAll()
        {
            using (var db = new DatasetContext())
            {

                return db.Dataset1.ToList();

            }
        }

        public Dataset_AnalogDigital GetLastAnalog()
        {

            return GetAll().Where(a => a.Code1 == 1).Last();

        }

    }
}
