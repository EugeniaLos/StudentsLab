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
        private List<ILogger> Loggers = new List<ILogger>();
        private Dictionary<ILogger, string[]> LoggersMapping = new Dictionary<ILogger, string[]>();
        private Dictionary<string, List<ILogger>> DependencyOnLevel = new Dictionary<string, List<ILogger>>();
        private static Logger _instance;

        public static Logger GetInstance()
        {
            return _instance ?? (_instance = new Logger());
        }

        private Logger()
        {
            string workingDirectory = Environment.CurrentDirectory;
            var builder = new ConfigurationBuilder()
             .SetBasePath(Directory.GetParent(workingDirectory).Parent.Parent.FullName)
             .AddJsonFile("appsettings.json");

            var config = builder.Build();

            CreateILoggerInstances(config);
            MadeDependencyOnLevel();
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
            if (DependencyOnLevel.ContainsKey("Error"))
            {
                foreach (ILogger instance in DependencyOnLevel["Error"])
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

        private void CreateILoggerInstances(IConfigurationRoot config)
        {
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
                        LoggersMapping.Add(Loggers[Loggers.Count-1], configuration[logger]);
                    }
                }
            }
        }

        private void MadeDependencyOnLevel()
        {
            foreach(ILogger instance in LoggersMapping.Keys)
            {
                foreach(string instanceLevel in LoggersMapping[instance])
                {
                    if (DependencyOnLevel.ContainsKey(instanceLevel))
                    {
                         DependencyOnLevel[instanceLevel].Add(instance);
                    }
                    else
                    {
                        DependencyOnLevel.Add(instanceLevel, new List<ILogger> { instance });
                    }
                }
            }
        }

        private void InvokeMethod(string level, string message)
        {
            if (DependencyOnLevel.ContainsKey(level))
            {
                foreach (ILogger instance in DependencyOnLevel[level])
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