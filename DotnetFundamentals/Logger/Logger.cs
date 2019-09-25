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
        private List<string> consoleLogLevel;
        private List<string> fileLogLevel;
        private ConsoleLogger consoleLogger = new ConsoleLogger();
        private FileLogger fileLogger = new FileLogger();
        //private ConfigurationBuilder builder;

        public Logger()
        {
            string workingDirectory = Environment.CurrentDirectory;
            var builder = new ConfigurationBuilder()
             .SetBasePath(Directory.GetParent(workingDirectory).Parent.Parent.FullName)
             .AddJsonFile("appsettings.json");

            var config = builder.Build();

            consoleLogLevel = config.GetSection("ConsoleLogger:LogLevel").Get<List<string>>();
            fileLogLevel = config.GetSection("FileLogger:LogLevel").Get<List<string>>();

        }

        public void Error(string message)
        {
            if (PermittedLevel("Error", consoleLogLevel))
            {
                consoleLogger.Error(message);
            }

            if (PermittedLevel("Error", fileLogLevel))
            {
                fileLogger.Error(message);
            }
        }

        public void Error(Exception ex)
        {
            if (PermittedLevel("Error", consoleLogLevel))
            {
                consoleLogger.Error(ex);
            }

            if (PermittedLevel("Error", fileLogLevel))
            {
                fileLogger.Error(ex);
            }
        }

        public void Warning(string message)
        {
            if (PermittedLevel("Warning", consoleLogLevel))
            {
                consoleLogger.Warning(message);
            }

            if (PermittedLevel("Warning", fileLogLevel))
            {
                fileLogger.Warning(message);
            }
        }

        public void Info(string message)
        {
            if (PermittedLevel("Info", consoleLogLevel))
            {
                consoleLogger.Info(message);
            }

            if (PermittedLevel("Info", fileLogLevel))
            {
                fileLogger.Info(message);
            }
        }

        private bool PermittedLevel(string level, List<string> permittedLevels)
        {
            if (permittedLevels == null)
            {
                return false;
            }
            foreach (string log in permittedLevels)
            {
                if (log == level)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
