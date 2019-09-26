using System;
using System.Collections.Generic;
using System.Text;

namespace Logger
{
    public class ConsoleLogger//: ILogger
    {
        public static void Error(string message)
        {
            Console.WriteLine(message);
        }

        public static void Error(Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

        public static void Warning(string message)
        {
            Console.WriteLine(message);
        }

        public static  void Info(string message)
        {
            Console.WriteLine(message);
        }
    }
}
