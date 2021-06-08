using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.DataAccess
{
    [ExcludeFromCodeCoverage]
    public class CustomLimitAccess
    {
        public bool Insert(Dataset_CustomLimit DA)
        {
            try
            {
                using (var db = new DatasetContext())
                {

                    db.Dataset2.Add(DA);
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

        public List<Dataset_CustomLimit> GetAll()
        {
            using (var db = new DatasetContext())
            {

                return db.Dataset2.ToList();

            }
        }

        public Dataset_CustomLimit GetLastCustom() 
        {
            return GetAll().Where(a => a.Code1 == 3).Last();
        }

        public Dataset_CustomLimit GetLastLimit()
        {
            return GetAll().Where(a => a.Code1 == 4).Last();
        }

    }
}
