using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Logger
{
    public class FileLogger//: ILogger
    {
        public static void Error(string message)
        {
            WriteToFile(message);
        }

        public static void Error(Exception ex)
        {
            WriteToFile(ex.ToString());
        }

        public static void Warning(string message)
        {
            WriteToFile(message);
        }

        public static void Info(string message)
        {
            WriteToFile(message);
        }

        private static void WriteToFile(string message)
        {
            using (StreamWriter sw = new StreamWriter(Path.Combine(Environment.CurrentDirectory, "loggerFile.txt"), true, System.Text.Encoding.Default))
            {
                sw.WriteLine(message);
            }
        }
    }
}
