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
        //private ConfigurationBuilder builder;

        public Logger()
        {
            string workingDirectory = Environment.CurrentDirectory;
            var builder = new ConfigurationBuilder()
             .SetBasePath(Directory.GetParent(workingDirectory).Parent.Parent.FullName)
             .AddJsonFile("appsettings.json");

            var config = builder.Build();

            var consoleLogLevel = config.GetSection("ConsoleLogger:LogLevel").Get<List<string>>();
            var fileLogLevel = config.GetSection("FileLogger:LogLevel").Get<List<string>>();
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
