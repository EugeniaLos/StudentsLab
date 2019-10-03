using System;

namespace Logger
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var l = Logger.GetInstance();
            l.Warning("Oopsie!!!");
            Console.ReadLine();
        }
    }
}
