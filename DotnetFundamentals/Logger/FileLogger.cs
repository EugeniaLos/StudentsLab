using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Logger
{
    public class FileLogger: ILogger
    {
        public void Error(string message)
        {
            WriteToFile(message);
        }

        public void Error(Exception ex)
        {
            WriteToFile(ex.ToString());
        }

        public void Warning(string message)
        {
            WriteToFile(message);
        }

        public void Info(string message)
        {
            WriteToFile(message);
        }

        private void WriteToFile(string message)
        {
            using (StreamWriter sw = new StreamWriter(Path.Combine(Environment.CurrentDirectory, "loggerFile.txt"), true, System.Text.Encoding.Default))
            {
                sw.WriteLine(message);
            }
        }
    }
}
