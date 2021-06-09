using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Logger
{
    public class Logger : ILogger
    {
        public void WriteToFile(string message)
        {
            string file = @"..\..\..\log.txt";
            using (StreamWriter sw = File.AppendText(file)) 
            {
                sw.WriteLine(message);
            }
        }
    }
}
