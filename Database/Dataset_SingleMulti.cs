using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Database
{
    [ExcludeFromCodeCoverage]
    public class Dataset_SingleMulti
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int Code1 { get; set; }
        public double Value1 { get; set; }

        public int IDWorker { get; set; }
        [Timestamp]
        public byte[] Timestamp { get; set; }
    }
}
