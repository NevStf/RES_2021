using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.DataAccess
{
    [ExcludeFromCodeCoverage]
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

        public Dataset_SingleMulti GetLastSingle()
        {
            return GetAll().Where(a => a.Code1 == 5).Last();
        }

        public Dataset_SingleMulti GetLastMulti()
        {
            return GetAll().Where(a => a.Code1 == 6).Last();
        }

    }
}
