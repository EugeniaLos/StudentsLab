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

        public void Error(string message)
        {
            WriteToPermittedDestination("Error", message);
        }

        public void Error(Exception ex)
        {
          
        }

        public void Warning(string message)
        {
            WriteToPermittedDestination("Warning", message);
        }

        public void Info(string message)
        {
            WriteToPermittedDestination("Info", message);
        }

        private void WriteToPermittedDestination(string desiredLevel, string message)
        {
            bool destinationProvided = false;
            foreach (string key in _configuration.Keys)
            {
                foreach (string level in _configuration[key])
                {
                    if (level == desiredLevel)
                    {
                        string typeName = "Logger." + key;
                        InvokeStringMethod(typeName, level, message);
                        destinationProvided = true;
                    }
                }
            }
            if (!destinationProvided)
            {
                InvokeStringMethod("Logger.ConsoleLogger", desiredLevel, message);
            }
        }

        private void InvokeStringMethod(string typeName, string methodName, string stringParam)
        {
            Type calledType = Type.GetType(typeName);
            calledType.InvokeMember(
                            methodName,
                            BindingFlags.InvokeMethod | BindingFlags.Public |
                                BindingFlags.Static,
                            null,
                            null,
                            new Object[] { stringParam });
        }
    }
}
