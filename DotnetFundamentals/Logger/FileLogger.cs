using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Logger
{
    public class FileLogger: ILogger
    {
        private StreamWriter streamWriter;
        public FileLogger()
        {
            if (streamWriter == null)
            {
                streamWriter = new StreamWriter(Path.Combine(Environment.CurrentDirectory, "loggerFile.txt"), true);
            }
        }

        public void Error(string message)
        {
            streamWriter.WriteLine(message);
        }

        public void Error(Exception ex)
        {
            streamWriter.WriteLine(ex.ToString());
        }

        public void Warning(string message)
        {
            streamWriter.WriteLine(message);
        }

        public void Info(string message)
        {
            streamWriter.WriteLine(message);
        }

        ~FileLogger()
        {
            streamWriter.Close();
        }
    }
}
