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
        private readonly Dictionary<string, List<ILogger>> _dependencyOnLevel;

        private static Logger _loggerInstance;
        private static readonly object syncRoot = new object();

        public static Logger Instance
        {
            get
            {
                if (_loggerInstance == null)
                {
                    lock (syncRoot)
                    {
                        if (_loggerInstance == null)
                        {
                            return _loggerInstance = new Logger();
                        }
                    }
                }
                return _loggerInstance;
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

            _dependencyOnLevel = new Dictionary<string, List<ILogger>>();
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
            if (_dependencyOnLevel.ContainsKey("Error"))
            {
                foreach (ILogger instance in _dependencyOnLevel["Error"])
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
            List<ILogger> loggers = new List<ILogger>();
            Dictionary<ILogger, string[]> loggersMapping = new Dictionary<ILogger, string[]>();
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
                        loggers.Add((ILogger)Activator.CreateInstance(t));
                        loggersMapping.Add(loggers[loggers.Count - 1], configuration[logger]);
                    }
                }
            }

            foreach (ILogger instance in loggersMapping.Keys)
            {
                foreach (string instanceLevel in loggersMapping[instance])
                {
                    if (_dependencyOnLevel.ContainsKey(instanceLevel))
                    {
                        _dependencyOnLevel[instanceLevel].Add(instance);
                    }
                    else
                    {
                        _dependencyOnLevel.Add(instanceLevel, new List<ILogger> { instance });
                    }
                }
            }
        }

        private void InvokeMethod(string level, string message)
        {
            if (_dependencyOnLevel.ContainsKey(level))
            {
                foreach (ILogger instance in _dependencyOnLevel[level])
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