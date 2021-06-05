using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class DatasetContext : DbContext 
    {
        public DbSet <Dataset_AnalogDigital> Dataset1 { get; set; }
        public DbSet <Dataset_CustomLimit> Dataset2 { get; set; }
        public DbSet <Dataset_SingleMulti> Dataset3 { get; set; }
        public DbSet <Dataset_ConsumerSource> Dataset4 { get; set; }
    }
}
