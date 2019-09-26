using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System.Reflection;

namespace Logger
{
    public class Logger: ILogger
    {
        private Dictionary<string, string[]> _configuration;

        public Logger()
        {
            string workingDirectory = Environment.CurrentDirectory;
            var builder = new ConfigurationBuilder()
             .SetBasePath(Directory.GetParent(workingDirectory).Parent.Parent.FullName)
             .AddJsonFile("appsettings.json");

            var config = builder.Build();

            _configuration = config.GetSection("Logging").Get<Dictionary<string, string[]>>();

        }

        public void Warning(string message)
        {
            var instances = GetLoggerInstance("Warning");
            foreach(ILogger instance in instances)
            {
                instance.Warning(message);
            }
        }

        public void Info(string message)
        {
            var instances = GetLoggerInstance("Info");
            foreach (ILogger instance in instances)
            {
                instance.Info(message);
            }
        }

        public void Error(string message)
        {
            var instances = GetLoggerInstance("Error");
            foreach (ILogger instance in instances)
            {
                instance.Error(message);
            }
        }

        public void Error(Exception ex)
        {
            var instances = GetLoggerInstance("Error");
            foreach (ILogger instance in instances)
            {
                instance.Error(ex);
            }
        }

        private IEnumerable<ILogger> GetLoggerInstance(string desiredLevel)
        {
            bool destinationProvided = false;
            foreach (string key in _configuration.Keys)
            {
                foreach (string level in _configuration[key])
                {
                    if (level == desiredLevel)
                    {
                        string typeName = "Logger." + key;
                        Type calledType = Type.GetType(typeName);
                        var instance = Activator.CreateInstance(calledType);
                        yield return (ILogger)instance;
                        destinationProvided = true;
                    }
                }
            }
            if (!destinationProvided)
            {
                var instance = new ConsoleLogger();
                yield return instance;
            }
        }
    }
}
