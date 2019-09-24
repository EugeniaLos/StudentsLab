using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Logger
{
    public class Logger: ILogger
    {
        private ILogger logger;
        private ConfigurationBuilder builder;

        public Logger()
        {
            this.logger = new ConsoleLogger();
        }

        public void Error(string message)
        {
            logger.Error(message);
        }
        public void Error(Exception ex)
        {
            logger.Error(ex);
        }
        public void Warning(string message)
        {
            logger.Warning(message);
        }
        public void Info(string message)
        {
            logger.Info(message);
        }
    }
}
