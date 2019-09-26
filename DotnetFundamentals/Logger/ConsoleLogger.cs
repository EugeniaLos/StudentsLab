using System;
using System.Collections.Generic;
using System.Text;

namespace Logger
{
    public class ConsoleLogger: ILogger
    {
        public void Error(string message)
        {
            Console.WriteLine(message);
        }

        public void Error(Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

        public void Warning(string message)
        {
            Console.WriteLine(message);
        }

        public void Info(string message)
        {
            Console.WriteLine(message);
        }
    }
}
