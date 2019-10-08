using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System.Reflection;
using System.Linq;

namespace Logger
{
    public class Logger: ILogger
    {
        private Dictionary<string, List<ILogger>> dependencyOnLevel = new Dictionary<string, List<ILogger>>();

        private static Logger loggerInstance;
        private static readonly object syncRoot = new object();

        public static Logger Instance
        {
            get
            {
                if (loggerInstance == null)
                {
                    lock (syncRoot)
                    {
                        if (loggerInstance == null)
                        {
                            return loggerInstance = new Logger();
                        }
                    }
                }
                return loggerInstance;
            }
        }

        private Logger()
        {
            string workingDirectory = Environment.CurrentDirectory;
            var builder = new ConfigurationBuilder()
             .SetBasePath(Directory.GetParent(workingDirectory).Parent.Parent.FullName)
             .AddJsonFile("appsettings.json");

            var config = builder.Build();

            CreateDictionary(config);
        }

        public void Warning(string message)
        {
            InvokeMethod("Warning", message);
        }

        public void Info(string message)
        {
            InvokeMethod("Info", message);
        }

        public void Error(string message)
        {
            InvokeMethod("Error", message);
        }

        public void Error(Exception ex)
        {
            if (dependencyOnLevel.ContainsKey("Error"))
            {
                foreach (ILogger instance in dependencyOnLevel["Error"])
                {
                    instance.Error(ex);
                }
            }
            else
            {
                var instance = new ConsoleLogger();
                instance.Error(ex);
            }
        }

        private void CreateDictionary(IConfigurationRoot config)
        {
            List<ILogger> Loggers = new List<ILogger>();
            Dictionary<ILogger, string[]> LoggersMapping = new Dictionary<ILogger, string[]>();
            var configuration = config.GetSection("Logging").Get<Dictionary<string, string[]>>();
            var type = typeof(ILogger);
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p));

            foreach (string logger in configuration.Keys)
            {
                foreach (Type t in types)
                {
                    if (logger == t.Name)
                    {
                        Loggers.Add((ILogger)Activator.CreateInstance(t));
                        LoggersMapping.Add(Loggers[Loggers.Count - 1], configuration[logger]);
                    }
                }
            }

            foreach (ILogger instance in LoggersMapping.Keys)
            {
                foreach (string instanceLevel in LoggersMapping[instance])
                {
                    if (dependencyOnLevel.ContainsKey(instanceLevel))
                    {
                        dependencyOnLevel[instanceLevel].Add(instance);
                    }
                    else
                    {
                        dependencyOnLevel.Add(instanceLevel, new List<ILogger> { instance });
                    }
                }
            }
        }

        private void InvokeMethod(string level, string message)
        {
            if (dependencyOnLevel.ContainsKey(level))
            {
                foreach (ILogger instance in dependencyOnLevel[level])
                {
                    Type t = instance.GetType();
                    MethodInfo method = t.GetMethod(level);
                    method.Invoke(instance, new object[] { message });
                }
            }
            else
            {
                var instance = new ConsoleLogger();
                Type t = instance.GetType();
                MethodInfo method = t.GetMethod(level);
                method.Invoke(instance, new object[] { message });
            }
        }
    }
}