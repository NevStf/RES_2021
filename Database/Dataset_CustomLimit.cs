﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class Dataset_CustomLimit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int Code1 { get; set; }
        public double Value1 { get; set; }
        public int Code2 { get; set; }
        public double Value2 { get; set; }

        [Timestamp]
        public byte[] Timestamp { get; set; }

        [Required, DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime Date { get; set; }



    }
}