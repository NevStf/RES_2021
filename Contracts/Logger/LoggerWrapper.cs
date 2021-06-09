using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Logger
{
    public class LoggerWrapper
    {
        private ILogger _logger;
        public LoggerWrapper(ILogger logger)
        {
            _logger = logger;
        }
        public void WriteToFile(string message)
        {
            _logger.WriteToFile(message);
        }
    }
}
